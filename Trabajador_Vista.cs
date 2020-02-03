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
        private readonly MonitorArchivos _observador;
        private SerialPortManager _portManager;
        private bool RecursosLiberados = false;
        public Session _MarcasUsuario;

        public Trabajador_Vista()
        {
            InitializeComponent();
            _observador = new MonitorArchivos(this,Cooperado);
            CheckForIllegalCrossThreadCalls = false;
            SettingUI();
            _observador.Run();
            //Trabajador.RunWorkerAsync();

            //Creamos la sesion
            _MarcasUsuario = new Session();
            _observador.InsertarProceso_Planta(_MarcasUsuario);
            OracleManager.SetConfiguracionDePuerto(_MarcasUsuario.Id_Planta,_MarcasUsuario.Tipo_Proceso);
            _portManager = new SerialPortManager();
            
            //Configurar la sesion cuando se abra maximizado 
            Configurar_Sesion();

        }

        //Inicializa la sesion con ID_Ticket, recepcion, planta y cooperado
        public void Configurar_Sesion()
        {
            SQliteManager.Configurar_Session(_MarcasUsuario);
            Cooperado.CooperadoINFO.Text = "Nombre Cooperado: " + _MarcasUsuario.Nombre_Cooperado;
            Cooperado.PlantaINFO.Text = "Planta: "+_MarcasUsuario.Nombre_Planta;
            Cooperado.TicketText.Text = _MarcasUsuario.Id_Ticket.ToString();
            TicketText.Text = _MarcasUsuario.Id_Ticket.ToString();
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

        void SettingUI()
        {
            IniciarButton.Enabled = false;
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

        private void AbortarClick(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Estás seguro de querer anular la toma de grado ?. " +
                    "Deberás reiniciar el proceso desde la página web.", "Anular Acción", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resultado == DialogResult.OK)
            {
                _MarcasUsuario.Clear_Session();
                WindowState = FormWindowState.Minimized;
            }
        }

        public void LiberandoRecursos()
        {
            if (!RecursosLiberados) {
                DialogResult resultado = MessageBox.Show("¿Estás seguro de querer anular la toma de grado ?. " +
                    "Deberás reiniciar el proceso desde la página web.", "Anular Acción", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (resultado == DialogResult.OK)
                {
                    RecursosLiberados = true;
                    //Aqui minimizar, botar los datos
                    _observador.Stop();
                    Cooperado.Close();
                    Application.Exit();
                }
            }
        }

        private void Cerrando(object sender, FormClosingEventArgs e)
        {
            LiberandoRecursos();
        }

        private void MedicionClick(object sender, EventArgs e)
        {
            _observador.EscribirPalabra_Archivo('D',@"C:/ROMANA/REFRACTO/ENVIO.txt");
            MedicionButton.Enabled = false;
        }

        private void CapturarClick(object sender, EventArgs e)
        {
            //Aqui abre le puerto serial y obtiene el grado
            //_portManager.SetearGrado(_MarcasUsuario);
            _observador.LeerArchivoMediciones(@"C:/ROMANA/REFRACTO/mediciones.txt", _MarcasUsuario);
            LlenarGrilla();
        }

        private void GuardarClick(object sender, EventArgs e)
        {
            ScreenManager.TomarPantallazo(_MarcasUsuario);
            SQliteManager.InsertarDatos(_MarcasUsuario);
            DataGridInfo.Rows.Clear();
            _MarcasUsuario.Clear_Session();
            _observador.Intentos_Session = 0;
            _observador.EscribirPalabra_Archivo('F', @"C:/ROMANA/REFRACTO/ENVIO.txt");
        }

        public void LlenarGrilla()
        {
            DataGridInfo.Rows.Add(_MarcasUsuario.Intento,
                _MarcasUsuario.Grado.ToString(),
                _MarcasUsuario.Temperatura.ToString(),
                _MarcasUsuario.Volumen.ToString(),
                _MarcasUsuario.Operacion) ;
        }

        private void RepetirClick(object sender, EventArgs e)
        {
            if (_MarcasUsuario.Intento <= 3)
            {
                ScreenManager.TomarPantallazo(_MarcasUsuario);
                SQliteManager.InsertarDatos(_MarcasUsuario);
                _observador.EscribirPalabra_Archivo('R',@"C:/ROMANA/REFRACTO/ENVIO.txt");

            }
            else
            {
                MedicionButton.Enabled = false;
                CapturarButton.Enabled = false;
                RepetirButton.Enabled = false;
            }
        }

        private void Loop_Trabajo(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (true) {
                if (SQliteManager.DebeEjecutarse())
                {
                    SQliteManager.CambiarEstado_App(2);
                    WindowState = FormWindowState.Maximized;

                }
                else
                {
                    WindowState = FormWindowState.Minimized;
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
