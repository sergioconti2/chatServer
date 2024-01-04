using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {
        private delegate void AtualizaStatusCallback(string strMensagem);

        bool conectado = false;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (conectado)
            {
                Application.Exit();
                return;
            }
            if (txtIP.Text == null)
            {
                MessageBox.Show("O endereço de IP é um campo de preenchimento obrigatório", "Alerta do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIP.Focus();
                return;
            }
            try
            {
                // Analisar o endereço de IP do servidor informado pelo textbox
                IPAddress enderecoIP = IPAddress.Parse(txtIP.Text);
                int portaHost = (int)txtPorta.Value;

                // Cria uma nova instância do objeto ChatServidor
                Servidor mainServidor = new Servidor(enderecoIP,portaHost);

                // Vincular o tratamento do evento StatusChanged a mainServer_StatusChanged
                Servidor.StatusChanged += new StatusChangedEventHandler(mainServidor_StatusChanged);

                // Iniciar atendimento 
                mainServidor.IniciaAtendimento();

                //Mostrar atendimento de conexões
                listLog.Items.Add("Servidor está online, Aguardando usuários conectarem...");
                listLog.SetSelected(listLog.Items.Count - 1, true);


            }
            catch (Exception ex)
            {

                listLog.Items.Clear();
                listLog.Items.Add("Erro de conexão..: "+ ex);
                listLog.SetSelected(listLog.Items.Count - 1, true);
                return;
            }
            conectado = true;
            txtIP.Enabled = false;
            txtPorta.Enabled = false;
            btnStartServer.ForeColor = Color.Red;
            btnStartServer.Text = "Sair...";
        }
        public void mainServidor_StatusChanged(object sender,StatusChangedEventArgs e) 
        {
            // Chama o metodo que atualiza o formulário
            this.Invoke(new AtualizaStatusCallback(this.AtualizaStatus),new object[] { e.EventMessage });

        
        }

        private void AtualizaStatus(string strMensagem)
        {
            // Atualiza a logo com mensagens
            listLog.Items.Add(strMensagem);
            listLog.SetSelected(listLog.Items.Count - 1,true);

        }
    }
}
