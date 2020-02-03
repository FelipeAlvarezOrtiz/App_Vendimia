using Romana_AppVendimia.Modelo;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Romana_AppVendimia
{
    public partial class Trabajador_Vista : Form
    {
        public readonly Screen[] screens = Screen.AllScreens;
        public readonly Vista_Cooperado Cooperado = new Vista_Cooperado();
        private bool Esperando = true;
        private readonly MonitorArchivos _observador;

        public Trabajador_Vista()
        {
            InitializeComponent();
            _observador = new MonitorArchivos(this,Cooperado);
            Control.CheckForIllegalCrossThreadCalls = false;
            SettingUI();
            _observador.Run();
            Esperar_Llamada();
        }

        public void Esperar_Llamada()
        {
            while (Esperando)
            {
                Esperando = SQliteManager.DebeEjecutarse();
                if (!Esperando)
                {
                    Esperando = false;
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
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
            WindowState = FormWindowState.Minimized;

        }

        void SettingUI()
        {
            var ValorObtenido = _observador.LeerPrimerChar_Archivo(@"C:/ROMANA/REFRACTO/RECEPCION.TXT");
            if (ValorObtenido.CompareTo('A') == 0)
            {
                _observador.estadoActual = EstadoPLC.A;
                EstadoText.Text = "EN ESPERA COMPLETAR NIVEL (5 LTS.)";
                CapturarButton.Enabled = false;
                MedicionButton.Enabled = false;
                RepetirButton.Enabled = false;
                GuardarButton.Enabled = false;
            }
            if (ValorObtenido.CompareTo('B') == 0)
            {
                _observador.estadoActual = EstadoPLC.B;
                EstadoText.Text = "APERTURA VALVULA DE SALIDA PARA COMPLETAR AMBIENTACIÓN.";
                CapturarButton.Enabled = false;
                MedicionButton.Enabled = false;
                RepetirButton.Enabled = false;
                GuardarButton.Enabled = false;
            }
            if (ValorObtenido.CompareTo('C') == 0)
            {
                _observador.estadoActual = EstadoPLC.C;
                EstadoText.Text = "VALVULA SALIDA SE CIERRA PARA COMPLETAR 7 LTS. MEDICIÓN.";
                CapturarButton.Enabled = false;
                MedicionButton.Enabled = false;
                RepetirButton.Enabled = false;
                GuardarButton.Enabled = false;
            }
            if (ValorObtenido.CompareTo('D') == 0)
            {
                _observador.estadoActual = EstadoPLC.D;
                EstadoText.Text = "SISTEMA LISTO PARA ADQUISICIÓN CONTIENE 7 LTS. O MÁS";
                CapturarButton.Enabled = false;
                MedicionButton.Enabled = true;
                RepetirButton.Enabled = false;
                GuardarButton.Enabled = false;
            }
            if (ValorObtenido.CompareTo('E') == 0)
            {
                _observador.estadoActual = EstadoPLC.E;
                EstadoText.Text = "SISTEMA LISTO PARA ADQUISICIÓN PERO BAJO NIVEL.";
                CapturarButton.Enabled = false;
                MedicionButton.Enabled = false;
                RepetirButton.Enabled = false;
                GuardarButton.Enabled = false;
            }
            if (ValorObtenido.CompareTo('F') == 0)
            {
                //ESTO TENER OJO, POR SI ESTA CONTINUAMENTE ESCRIBIENDO LETRAS
                _observador.estadoActual = EstadoPLC.F;
                EstadoText.Text = "SISTEMA LISTO PARA RECIBIR TÉRMINO DE ADQUISICIÓN.";
                CapturarButton.Enabled = true;
                MedicionButton.Enabled = false;
                RepetirButton.Enabled = true;
                GuardarButton.Enabled = false;
            }
        }

    }
}
