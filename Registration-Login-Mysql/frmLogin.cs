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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        string sqlServidor = "";
        string sqlUsuario = "";
        string sqlSenha = "";
        string sqlNomeDb = "";

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int retorno = -1;

            MySqlConnection conn = new MySqlConnection(@"SERVER=" + sqlServidor + "; DATABASE=" + sqlNomeDb + "; UID=" + sqlUsuario + "; PASSWORD=" + sqlSenha + ";");
            string comando = "SELECT COUNT(*) FROM users WHERE tbUsername=@tbUsername AND tbPassword=@tbPassword";
            MySqlCommand cmd = new MySqlCommand(comando, conn);

            cmd.Parameters.AddWithValue("@tbUsername", txtUserName.Text);
            cmd.Parameters.AddWithValue("@tbPassword", txtPassword.Text);

            conn.Open();
            retorno = Convert.ToInt32(cmd.ExecuteScalar());

            if (retorno > 0)
            {
                MessageBox.Show("Login successfully!", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                frmPanel formPanel = new frmPanel();

                formPanel.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credentials are incorrect!", "Important Note", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            ObterCredenciaisDoSql();
        }
    }
}
