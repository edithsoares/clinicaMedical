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
        private void txtUser_Enter_1(object sender, EventArgs e)
        {
            txtUser.Text = "";
            txtUser.ForeColor = Color.LightGray;
        }

        private void txtUser_Leave(object sender, EventArgs e)
        {
            txtUser.ForeColor = Color.Silver;
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = false;
            txtPass.ForeColor = Color.LightGray;
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = false;
            txtPass.ForeColor = Color.Silver;
        }
        private void ptbMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ptbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Arrastar o FormLogin
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
                // Verifica campos com valor maior que 2
                if (txtUser.Text != "Username" && txtUser.TextLength > 2)
                {
                    // Validação da senha
                    if (txtPass.Text != "Password")
                    {
                        UserModel user = new UserModel();
                        var validLogin = user.LoginUser(txtUser.Text, txtPass.Text);
                        if (validLogin == true)
                        {
                            FormMainMenu mainMenu = new FormMainMenu();
                            MessageBox.Show("Bem Vindo " + CacheDoUsuario.FirstName + " " + CacheDoUsuario.Sobrenome);
                            mainMenu.Show();
                            mainMenu.FormClosed += Logout;
                            this.Hide();
                        }
                        else
                        {
                            msgError("Nome de usuário ou senha incorreta inserida. \n Por favor, tente novamente.");
                            txtPass.Text = " ";
                            txtPass.UseSystemPasswordChar = false;
                            txtUser.Focus();
                        }
                    } // Se campos vazios
                    else msgError("Por favor, digite a senha.");
                }
                else msgError("Digite o nome de usuário.");
            }

            // Método msg de erro
            private void msgError(string msg)
            {
                lblMsgError.Text = "    " + msg;
                lblMsgError.Visible = true;
            }

            // Método logout, voltar a tela de login
            private void Logout(object sender, FormClosedEventArgs e)
            {
                
                
                txtPass.Text = "";
                txtPass.UseSystemPasswordChar = true;
                txtUser.Text = "";
                lblMsgError.Visible = false;
                this.Show();
                CacheDoUsuario.Password = " ";
                CacheDoUsuario.LoginName = " ";
        }     
    }
}

