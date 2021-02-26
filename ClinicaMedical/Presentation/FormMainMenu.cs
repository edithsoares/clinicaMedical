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

namespace Presentation
{
    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();

            // Estas linhas eliminam a oscilação do formulário ou controles na interface gráfica (mas não 100%)
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }

        //funcionalidade de redimensionar o formulário

        // RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

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

        //DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);

            this.pnlConteiner.Region = region;
            this.Invalidate();

        }

        //COLOR Y GRIP DE RECTANGULO INFERIOR

        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));

            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);

            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);

        }
        private void ptbCancel_Click(object sender, EventArgs e)
        {
            // Fechar Aplicação
            Application.Exit();
        }

        // Capture a posição e o tamanho antes de maximizar para restaurar
        int lx, ly;
        int sw, sh;

       

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

    }
    //// Criar formulário de arrasto
    //[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    //private extern static void ReleaseCapture();

    //[DllImport("user32.DLL", EntryPoint = "SendMessage")]
    //private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

    //private void panelBarraTitulo_MouseMove(object sender, MouseEventArgs e)
    //{
    //    ReleaseCapture();
    //    object p = SendMessage(this.Handle, 0x112, 0xf012, 0);
    //}
}
