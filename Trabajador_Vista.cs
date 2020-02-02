using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Romana_AppVendimia
{
    public partial class Trabajador_Vista : Form
    {
        public readonly Screen[] screens = Screen.AllScreens;
        public Vista_Cooperado Cooperado = new Vista_Cooperado();
        public Trabajador_Vista()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Load_Refracto(object sender, EventArgs e)
        {
            Setting_Monitores();
        }

        void Setting_Monitores()
        {
            Rectangle bounds = screens[1].Bounds;
            Cooperado.SetBounds(bounds.X,bounds.Y,1200,1200);
            Cooperado.StartPosition = FormStartPosition.Manual;
            Cooperado.Show();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            Cooperado.WindowState = FormWindowState.Maximized;
            WindowState = FormWindowState.Maximized;

        }
    }
}
