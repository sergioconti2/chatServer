using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    // Esta classe trata as conexões , serão tantas quanto as instãncias do usuarios conectados 
    class Conexao
    {
        TcpClient tcpCliente;

        // a thread que enviará a mensagem para o cliente
        private Thread thrSender;
        private StreamReader srReceptor;
        private StreamWriter srTransmissor;
        private string usuarioAtual;
        private string strResposta;


        // Construtor da classe que toma a conexão TCP

        public Conexao(TcpClient tcpCon)
        {
            tcpCliente = tcpCon;
            // A thread que aceita o cliente e espera mensagem
            thrSender = new Thread(AceitaCliente);
            thrSender.IsBackground = true;
            // A thread chama o metodo AceitaCliente()
            thrSender.Start();
        
        }
        private void FecharConexao()
        {
            //Fecha os objetos
            tcpCliente.Close();
            srReceptor.Close();
            srTransmissor.Close();
        }

        private void AceitaCliente()
        {
            srReceptor = new StreamReader(tcpCliente.GetStream());
            srTransmissor = new StreamWriter(tcpCliente.GetStream());

            // Lê a informação da conta do cliente
            usuarioAtual = srReceptor.ReadLine();

            // Tem uma resposta do cliente

            if (usuarioAtual != null)
            {
                // Armazena o nome do usuario na hashtable 
                if (Servidor.htUsuarios.Contains(usuarioAtual)== true)
                {
                    // O 0(zero)=> significa não conectado
                    srTransmissor.WriteLine("0| o usuário já existe...");
                    srTransmissor.Flush();
                    FecharConexao();
                    return;
                }
                else if (usuarioAtual == "Administrador")
                {
                    // O 0(zero)=> significa não conectado
                    srTransmissor.WriteLine("0|Este nome de usuário é reservado");
                    srTransmissor.Flush();
                    FecharConexao();
                    return;
                }
                else
                {
                    // 1 => significa que conectou com sucesso 
                    srTransmissor.WriteLine("1");
                    srTransmissor.Flush();

                    // Inclui o usuario na hash table e inicia a escuta das suas mensagens
                    Servidor.IncluiUsuario(tcpCliente,usuarioAtual);

                }
            }
            else
            {
                FecharConexao();
                return;
            }
            try
            {
                // Continua aguardando uma mensagem do usuàrio...
                while ((strResposta = srReceptor.ReadLine()) != "")
                {
                    // Se for inválido remove-o
                    if (strResposta == null)
                    {
                        Servidor.RemoverUsuario(tcpCliente);
                    }
                    else
                    {
                        // Envia mensagem para todos os usuarios
                        Servidor.EnviaMensagem(usuarioAtual,strResposta);
                    }
                }
            }
            catch 
            {

                // Se houver problemas com usuario , desconecteo
                Servidor.RemoverUsuario(tcpCliente); 

            }
        }




    }
}
