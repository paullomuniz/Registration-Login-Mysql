using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registration_Login_Mysql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            frmRegistration formRegistration = new frmRegistration();

            formRegistration.Show();

            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmLogin formLogin = new frmLogin();

            formLogin.Show();

            this.Hide();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            System.Diagnostics.Process.Start("notepad.exe", appPath + "\\config.txt");
        }
    }
}
