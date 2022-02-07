using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registration_Login_Mysql
{
    public partial class frmRegistration : Form
    {
        public frmRegistration()
        {
            InitializeComponent();
        }

        string sqlServidor = "";
        string sqlUsuario = "";
        string sqlSenha = "";
        string sqlNomeDb = "";

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            ObterCredenciaisDoSql();
        }

        private void ObterCredenciaisDoSql()
        {
            StreamReader sr = File.OpenText("config.txt");
            string text = sr.ReadToEnd();

            string[] resultadoServidor = text.Split(new string[] { "<servidor>", "</servidor>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in resultadoServidor)
            {
                if (s.Contains("<")) { }
                else
                {
                    sqlServidor = s;
                }
            }

            string[] resultadoNomeDb = text.Split(new string[] { "<nomeDb>", "</nomeDb>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in resultadoNomeDb)
            {
                if (s.Contains("<")) { }
                else
                {
                    sqlNomeDb = s;
                }
            }

            string[] resultadoUsuario = text.Split(new string[] { "<usuario>", "</usuario>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in resultadoUsuario)
            {
                if (s.Contains("<")) { }
                else
                {
                    sqlUsuario = s;
                }
            }

            string[] resultadoSenha = text.Split(new string[] { "<senha>", "</senha>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in resultadoSenha)
            {
                if (s.Contains("<")) { }
                else
                {
                    sqlSenha = s;
                }
            }

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if(txtUserName.Text != "" && txtPassword.Text != "")
            {
                MySqlConnection connection = new MySqlConnection(@"SERVER=" + sqlServidor + "; DATABASE=" + sqlNomeDb + "; UID=" + sqlUsuario + "; PASSWORD=" + sqlSenha);
                MySqlCommand command;

                string queryDocumentacao = "INSERT INTO users(tbUsername,tbPassword) VALUES ('" + txtUserName.Text + "','" + txtPassword.Text + "');";
                command = new MySqlCommand(queryDocumentacao, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Registration successful!", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                frmLogin formLogin = new frmLogin();
                formLogin.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Fill in all fields!", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            
        }
    }
}
