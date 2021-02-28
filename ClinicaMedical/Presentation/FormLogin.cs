using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Domain;
using Common.Cache;

namespace Presentation
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        // Configurações de Desing do Form
        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtUser.Text == "User")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.LightGray;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                txtUser.Text = "User";
                txtUser.ForeColor = Color.Silver;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Pass")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.LightGray;
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Pass";
                txtPass.ForeColor = Color.Silver;
                txtPass.UseSystemPasswordChar = true;
            }
        }
        private void ptbMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ptbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Arrastar o Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        // Funcionalidades dos Forms
        private void btnLogin_Click(object sender, EventArgs e)
        {
             
                if (txtUser.Text != "Username" && txtUser.TextLength > 2)
                {
                    if (txtPass.Text != "Password")
                    {
                        UserModel user = new UserModel();
                        var validLogin = user.LoginUser(txtUser.Text, txtPass.Text);
                        if (validLogin == true)
                        {
                            FormMainMenu mainMenu = new FormMainMenu();
                            MessageBox.Show("Bem Vindo " + CacheDoUsuario.FirstName + ", " + CacheDoUsuario.Sobrenome);
                            mainMenu.Show();
                            mainMenu.FormClosed += Logout;
                            this.Hide();
                        }
                        else
                        {
                            msgError("Incorrect username or password entered. \n   Please try again.");
                            txtPass.Text = " ";
                            txtPass.UseSystemPasswordChar = false;
                            txtUser.Focus();
                        }
                    }
                    else msgError("Please enter password.");
                }
                else msgError("Please enter username.");
            }

            private void msgError(string msg)
            {
                lblMsgError.Text = "    " + msg;
                lblMsgError.Visible = true;
            }

            private void Logout(object sender, FormClosedEventArgs e)
            {
                txtPass.Text = "Password";
                txtPass.UseSystemPasswordChar = false;
                txtUser.Text = "Username";
                lblMsgError.Visible = false;
                this.Show();
            }

        private void txtUser_Enter_1(object sender, EventArgs e)
        {

        }
    }
}

