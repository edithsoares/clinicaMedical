using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using Common.Cache;

namespace Presentation
{
    //
    // - Camada de apresentação
    //

    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();

            // Estas linhas eliminam a oscilação do formulário ou controles na interface gráfica (mas não 100%)
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }


        //
        //Segurança contra ataques
        //

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            ManagePermissionss();
            Security();
        }

        private void Security()
        {
            var userModel = new UserModel();
            if (userModel.SecurityLogin() == false)
            {
                MessageBox.Show("Erro fatal, foi detectado que você está tentando acessar o sistema sem credenciais, faça o login e identifique-se");
                Application.Exit();
            }
        }

        //
        // - Segurança de acordo com os tipos ou posições / Cargos dos usuários.
        //

        // Verifica Posição / Cargo de user
        public void ManagePermissionss()
        {
            if (CacheDoUsuario.Cargo == Positions.Receptionist)
            {
                // Restrição de acesso ao user.
                btnConfig.Enabled = false;
            }
           if (CacheDoUsuario.Cargo == Positions.Accounting)
            {
                // Restrição de acesso ao user.
                btnPaciente.Enabled = false;
                btnHistorico.Enabled = false;
                btnConfig.Enabled = false;
            }
            if (CacheDoUsuario.Cargo == Positions.Administrator)
            {
                // Restrição de acesso ao user
                // Sem restrição por enquanto
            }

        }

        //
        // Desing
        //

        // Redimencionar o tamanho para a forma no tempo de execuçãos
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        // Capturar a posição e o tamanho antes de maximizar para restauração
        int lx, ly;
        int sw, sh;

        // Redimencionar o tamanho para a forma no tempo de execução
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        //Desenha retangulo / Excluir painel de canto
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);

            this.pnlConteiner.Region = region;
            this.Invalidate();

        }

        //Cor e grip têtangulo inferior
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));

            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);

            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);

        }
        
        // Botões Maximizar / Minimizar / Restaurar e Encerrar
        private void ptbCancel_Click(object sender, EventArgs e)
        {
            // Fechar Aplicação
            Application.Exit();
        }

        private void ptbMaximize_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;

            ptbMaximize.Visible = false;
            ptbRestaurar.Visible = true;
            
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            
        }

        private void ptbRestaurar_Click(object sender, EventArgs e)
        {
            ptbMaximize.Visible = true;
            ptbRestaurar.Visible = false;

            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
        }

        private void ptbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Botôes para abris os formularios
        private void btnPaciente_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormPaciente>();
            btnPaciente.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnHistorico_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormHistorico>();
            btnPaciente.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormCalendario>();
            btnPaciente.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormConfig>();
            btnPaciente.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            AbrirFormulario<FormChat>();
            btnPaciente.BackColor = Color.FromArgb(12, 61, 92);
        }

        private void pnlBarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // Criar formulário de arrasto // Usa Dll interna
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();


        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        // Não deu certo AINDA
        //private void LoadUserData()
        //{
        //    lblUsername.Text = .LoginName;
        //    lblPosition.Text = UserCache.Position;
        //    lblEmail.Text = UserCache.Email;
        //}

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza de que deseja sair?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        

        // Método para abrir os formulários dentro do painel
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = pnlFormularios.Controls.OfType<MiForm>().FirstOrDefault();// Procure o formulário na coleção
                                                                                   // Se o formulário / instância não existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                pnlFormularios.Controls.Add(formulario);
                pnlFormularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
            }
            // Se o formulário / instância existe
            else
            {
                formulario.BringToFront();
            }
        }


        // Método para resturar a cor original do botão
        private void CloseForms(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["FormPaciente"] == null )            
                btnPaciente.BackColor = Color.FromArgb(4, 41, 68);            
            if (Application.OpenForms["FormHistorico"] == null)           
                btnPaciente.BackColor = Color.FromArgb(4, 41, 68);           
            if (Application.OpenForms["FormCalendario"] == null)            
                btnPaciente.BackColor = Color.FromArgb(4, 41, 68);           
            if (Application.OpenForms["FormConfig"] == null)           
                btnPaciente.BackColor = Color.FromArgb(4, 41, 68);            
            if (Application.OpenForms["FormChat"] == null)           
                btnPaciente.BackColor = Color.FromArgb(4, 41, 68);         
        }
    }
}
