using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    // Este delegate é necessário para especificar os parametros que estamos passando com o nosso evento
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

    class Servidor
    {
        //Esta hash table armazena  os usuarios e as conexões(acessado / consultado pelo usuario)
        public static Hashtable htUsuarios = new Hashtable(30);// 30 é o número máximo de usuários permitidos
                                                               //Esta hash table armazena  os usuarios e as conexões(acessado / consultado por conexão)) 
        public static Hashtable htConexoes = new Hashtable(30);// 30 é o número máximo de conexões permitidas
                                                               // armazena o endereço de IP
        private IPAddress enderecoIP;
        private int portaHost;
        private TcpClient tcpCliente;

        // O evento e o seu argumento irá notificar o formulário quando um usuário se conecta. 
        public static event StatusChangedEventHandler StatusChanged;
        private static StatusChangedEventArgs e;

        // O construtor define o endereço IP para aquele retorna pela instaciação do objeto.
        public Servidor(IPAddress endereco, int porta)
        {
            enderecoIP = endereco;
            portaHost = porta;
        }

        // Esta thread que irá tratar o escutador de conexões
        private Thread thrListener;

        // Este objeto TCP object que escuta as conexões
        private TcpListener tlsCliente;

        //Irá informar ao laço while para manter a monitoração das conexões
        bool ServerRodando = false;

        //Inclui o usuario nas tabelas hash
        public static void IncluiUsuario(TcpClient tcpUsuario, string strUserName)
        {
            // Primeiro inclui o nome e a conexão associada para ambas as hashs tables
            Servidor.htUsuarios.Add(strUserName, tcpUsuario);
            Servidor.htConexoes.Add(tcpUsuario, strUserName);

            //Informa a novaconexão a todos usuarios e para o formuláriodo servidor.
            EnviaMensagemAdmin(htConexoes[tcpUsuario] + " entrou...");
        }
        public static void RemoverUsuario(TcpClient tcpUsuario) 
        {
            //Remove o usuario das tabelas (hashes tables), isso se o usuario existir
            if (htConexoes[tcpUsuario] != null)
            {
                //Primeiro mostra a informação e informa para os outros sobre a conexao
                EnviaMensagemAdmin(htConexoes[tcpUsuario] + " saiu...");
                //Remover usuario
                Servidor.htUsuarios.Remove(Servidor.htConexoes[tcpUsuario]);
                Servidor.htUsuarios.Remove(tcpUsuario);
            }

        
        }
        //Este evento é chamado para disparar o evento StatusChanged
        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler = StatusChanged;

            if (statusHandler != null)
            {
                // Invoca o delegate
                statusHandler(null, e);
            }
        }
        public static void EnviaMensagemAdmin(string mensagem) 
        {
            StreamWriter swSenderSender;

            //exibe primeiro na exibixão
            e = new StatusChangedEventArgs("Administrador: " + mensagem);
            OnStatusChanged(e);

            //Cria um array de clientes TCP´s do tamanho de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[Servidor.htUsuarios.Count];
            //Copia os objetos TcpClient no array
            Servidor.htUsuarios.Values.CopyTo(tcpClientes,0);

            //Comando For para percorrer a lista de TCP de clientes
            for (int i = 0; i < tcpClientes.Length; i++)
            {
                //Tenta enviar uma mensagem para cada cliente
                try
                {
                    // Se a mensagem for nula ou conexão for nula sai
                    if (mensagem.Trim() == "" || tcpClientes[i] == null )
                    {
                        continue;
                    }

                    //Envia uma mensagem para o cliente atual no laço
                    swSenderSender = new StreamWriter(tcpClientes[i].GetStream());
                    swSenderSender.WriteLine("Administrador: "+ mensagem);
                    swSenderSender.Flush();
                    swSenderSender = null;


                }
                catch
                {
                    //Se houver um problema , o usuario não existe , então delete-o
                    RemoverUsuario(tcpClientes[i]);
                    
                }
            }
        }
        // Envia mensagens de um usuário para todos os outros
        public static void EnviaMensagem(string Origem , string Mensagem)
        {
            StreamWriter swWriter;

            //Primeiro exibe a mensagem na aplicação
            e = new StatusChangedEventArgs(Origem + " disse: " + Mensagem);
            OnStatusChanged(e);

            //Cria um array de clientes TCP´s do tamanho de clientes existentes
            TcpClient[] tcpClientes = new TcpClient[Servidor.htUsuarios.Count];
            //Copia os objetos TcpClient no array
            Servidor.htUsuarios.Values.CopyTo(tcpClientes, 0);

            //Comando For para percorrer a lista de TCP de clientes
            for (int i = 0; i < tcpClientes.Length; i++)
            {
                //Tenta enviar uma mensagem para cada cliente
                try
                {
                    // Se a mensagem for nula ou conexão for nula sai
                    if (Mensagem.Trim() == "" || tcpClientes[i] == null)
                    {
                        continue;
                    }

                    //Envia uma mensagem para o cliente atual no laço
                    swWriter = new StreamWriter(tcpClientes[i].GetStream());
                    swWriter.WriteLine(Origem +" disse: " + Mensagem);
                    swWriter.Flush();
                    swWriter = null;


                }
                catch
                {
                    //Se houver um problema , o usuario não existe , então delete-o
                    RemoverUsuario(tcpClientes[i]);

                }
            }
        }
        // Envia mensagens de um usuário para todos os outros
        public void IniciaAtendimento()
        {
            try
            {
                // Pega o numero de IP e porta local do cliente
                IPAddress ipLocal = enderecoIP;
                int portaLocal = portaHost;
                
                //Cria um objeto TCP Listener usando o IP do servidor e porta definidas.
                tlsCliente = new TcpListener(ipLocal,portaLocal);

                //Iniciar TCP Listener para escutar as conexões dos clientes.
                tlsCliente.Start();

                // O laço do while verifica se servidor está rodando antes de checar as conexões
                ServerRodando = true;

                //Inicia uma thread que hospeda o listener
                thrListener = new Thread(MantemAtendimento);
                thrListener.IsBackground = true;
                thrListener.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: "+ ex);
               
            }
        }

        private void MantemAtendimento()
        {
            //Enquanto o servidor estiver rodando
            while (ServerRodando)
            {
                // Aceita ua conexão pendente
                tcpCliente = tlsCliente.AcceptTcpClient();
                // Cria um nova instância de conexão
                Conexao newConnection = new Conexao(tcpCliente); 

            }
        }
    }
}


            