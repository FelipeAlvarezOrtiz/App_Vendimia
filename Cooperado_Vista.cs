using System;
using System.Windows.Forms;

namespace Romana_AppVendimia
{
    public partial class Vista_Cooperado : Form
    {
        public Vista_Cooperado()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void LoadForm(object sender, EventArgs e)
        {
            FechaText.Text = DateTime.Now.ToString("dd-MM-yyyy");
            HoraText.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void TickEvent(object sender, EventArgs e)
        {
            HoraText.Text = DateTime.Now.ToString("HH:mm:ss");
            if (Screen.AllScreens.Length > 1) {

                if (WindowState == FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Maximized;
                } 
            }
            else
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void Vista_Cooperado_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }

        }
    }//3858
}
