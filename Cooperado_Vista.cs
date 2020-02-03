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
        }
    }
}
