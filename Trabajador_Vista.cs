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
        public Vista_Cooperado cooperado = new Vista_Cooperado();
        public Trabajador_Vista()
        {
            InitializeComponent();
        }

        private void Load_Refracto(object sender, EventArgs e)
        {
            Setting_Monitores();
        }

        void Setting_Monitores()
        {
            Rectangle bounds = screens[1].Bounds;
            cooperado.SetBounds(bounds.X,bounds.Y,1200,1200);
            cooperado.StartPosition = FormStartPosition.Manual;
            cooperado.Show();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            cooperado.WindowState = FormWindowState.Maximized;
            WindowState = FormWindowState.Maximized;

        }
    }
}
