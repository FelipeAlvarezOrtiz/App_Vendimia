using Romana_AppVendimia.Modelo;
using System;
using System.Drawing;
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

        #region Prueba
        private string strBufferIn = string.Empty;
        private string strBufferOut = string.Empty;


        #endregion

        public Trabajador_Vista()
        {
            InitializeComponent();
                
            _observador = new MonitorArchivos(this,Cooperado);
            CheckForIllegalCrossThreadCalls = false;
            SettingUI();
            _observador.Run();
            SQliteManager.CheckDataBase();
            //Creamos la sesion
            _MarcasUsuario = new Session();
            _observador.InsertarProceso_Planta(_MarcasUsuario);
            OracleManager.SetConfiguracionDePuerto(_MarcasUsuario.Id_Planta,_MarcasUsuario.Tipo_Proceso);
            _portManager = new SerialPortManager();
            //Configurar la sesion cuando se abra maximizado 
            //Configurar_Sesion();
        }

        //Inicializa la sesion con ID_Ticket, recepcion, planta y cooperado
        public void Configurar_Sesion()
        {
            SQliteManager.Configurar_Session(_MarcasUsuario);
            Cooperado.CooperadoINFO.Text = "Nombre Cooperado: " + _MarcasUsuario.Nombre_Cooperado;
            Cooperado.PlantaINFO.Text = "Planta: "+_MarcasUsuario.Nombre_Planta;
            Cooperado.RutInfo.Text = "R.U.T: "+_MarcasUsuario.RUT_Cooperado;
            Cooperado.TicketText.Text = _MarcasUsuario.Id_Ticket.ToString();
            TicketText.Text = _MarcasUsuario.Id_Ticket.ToString();
            SQliteManager.CambiarEstado_Data(2, _MarcasUsuario);
        }

        private void Load_Refracto(object sender, EventArgs e)
        {
            Setting_Monitores();
        }

        void Setting_Monitores()
        {
            Rectangle bounds;
            if (screens.Length > 1)
                bounds = screens[1].Bounds;
            else
                bounds = screens[0].Bounds;
            Cooperado.SetBounds(bounds.X,bounds.Y,1200,1200);
            Cooperado.StartPosition = FormStartPosition.Manual;
            Cooperado.Show();
            MaximizeBox = false;
            MinimizeBox = false;
            Cooperado.WindowState = FormWindowState.Maximized;
            WindowState = FormWindowState.Minimized;

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
                Limpiar_UI();
                SQliteManager.CambiarEstado_App(4);
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
            if (_MarcasUsuario.Intento >= 3)
            {
                CapturarButton.Enabled = false;
                RepetirButton.Enabled = false;
            }
        }

        private void GuardarClick(object sender, EventArgs e)
        {
            InsertarEnBasesDeDatos(_MarcasUsuario);
            _MarcasUsuario.Clear_Session();
            _observador.Intentos_Session = 0;
            _observador.EscribirPalabra_Archivo('F', @"C:/ROMANA/REFRACTO/ENVIO.txt");
            Limpiar_UI();
            MessageBox.Show("Los datos se han guardado con exito en la base de datos.","Exito",
                MessageBoxButtons.OK,MessageBoxIcon.Information);
            SQliteManager.CambiarEstado_App(3);
        }

        private void Limpiar_UI()
        {
            #region Area de Cooperado
            Cooperado.TicketText.Text = "0";
            Cooperado.LecturaText.Text = "0";
            Cooperado.TemperaturaText.Text = "0.0";
            Cooperado.VolumenText.Text = "0.0";
            Cooperado.GradoText.Text = "0.0";
            Cooperado.RutInfo.Text = "RUT: No hay Información";
            Cooperado.PlantaINFO.Text = "Planta: No hay Información";
            Cooperado.CooperadoINFO.Text = "Cooperado: No hay Información";
            #endregion

            #region Area de Trabajador
            LecturaText.Text = "1";
            TicketText.Text = "1";
            DataGridInfo.Rows.Clear();
            #endregion
        
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
                //falta id_Cooperado != RUT
                InsertarEnBasesDeDatos(_MarcasUsuario);
                Limpiar_Repeticion();
                _observador.EscribirPalabra_Archivo('R',@"C:/ROMANA/REFRACTO/ENVIO.txt");

            }
            else
            {
                MedicionButton.Enabled = false;
                CapturarButton.Enabled = false;
                RepetirButton.Enabled = false;
            }
        }

        private void Limpiar_Repeticion()
        {
            Cooperado.GradoText.Text = "00.0";
            Cooperado.VolumenText.Text = "0.0";
            Cooperado.TemperaturaText.Text = "0.0";
            _MarcasUsuario.Volumen = 0;
            _MarcasUsuario.Temperatura = 0;
            _MarcasUsuario.Grado = 0;
        }

        private void InsertarEnBasesDeDatos(Session _userSession)
        {
            _MarcasUsuario.Hora = DateTime.Now.ToString("HH:mm:ss");
            _MarcasUsuario.Fecha = DateTime.Now.ToString("dd-MM-yyyy");
            ScreenManager.TomarPantallazo(_userSession);
            SQliteManager.InsertarDatos(_userSession);
            OracleManager.InsertarDatosEnPasarela(_userSession);
            OracleManager.InsertarFotoRecepcion(_userSession);
        }

        private void Consultor_Tick(object sender, EventArgs e)
        {
            if (SQliteManager.DebeEjecutarse())
            {
                SQliteManager.CambiarEstado_App(2);
                this.WindowState = FormWindowState.Maximized;
                Configurar_Sesion();
            }
            if(SQliteManager.DebeMinimizarse())
            {
                Limpiar_UI();
                WindowState = FormWindowState.Minimized;
                Console.WriteLine("No hay trabajo para realizar.");
            }
        }

        private void DataRecibida(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }
    }
}
