using Romana_AppVendimia.Modelo;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace Romana_AppVendimia
{
    public partial class Trabajador_Vista : Form
    {
        public readonly Screen[] screens = Screen.AllScreens;
        public readonly Vista_Cooperado Cooperado = new Vista_Cooperado();
        private readonly MonitorArchivos _observador;
        private static int ID_Planta_Revision = 0;
        private bool RecursosLiberados = false;
        public Session _MarcasUsuario;
        private static readonly string ConfigPath = @"C:/ROMANA/config.txt";
        //private static readonly string lockPath = @"C:/ROMANA/debug.lock";
        private static readonly string PathLog = @"C:/ROMANA/LOG/log.txt";


        #region Prueba
        private string strBufferIn = string.Empty;
        private delegate void DelegadoAcceso(string Accion);
        #endregion

        public Trabajador_Vista()
        {
            if (File.Exists(ConfigPath)) {
                InitializeComponent();
                _observador = new MonitorArchivos(this, Cooperado);
                CheckForIllegalCrossThreadCalls = false;
                SettingUI();
                _observador.Run();
                SQliteManager.CheckDataBase();
                ID_Planta_Revision = ConfirmarPlanta();
                _MarcasUsuario = new Session();
                _observador.InsertarProceso_Planta(_MarcasUsuario);
                OracleManager.SetConfiguracionDePuerto(PuertoSerial, _MarcasUsuario.Id_Planta, _MarcasUsuario.Tipo_Proceso);
                ConfigurarSerial();
                Cooperado.TopMost = true;
                TopMost = true;
                //SetAutoRun();
                try
                {
                    PuertoSerial.Open();
                }
                catch (Exception _error)
                {
                    MessageBox.Show("PuertoSerial mal configurado. Error: "+_error.Message,"Error",
                        MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Application.Exit();
                }
                GC.KeepAlive(PuertoSerial);
            }
            else
            {
                MessageBox.Show("NO EXISTE ARCHIVO DE CONFIGURACIONES. GENERAR ARCHIVO .CONFIG","ERROR",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        //Inicializa la sesion con NUM_TICKET, recepcion, planta y cooperado
        public void Configurar_Sesion()
        {
            SQliteManager.Configurar_Session(_MarcasUsuario);
            Cooperado.CooperadoINFO.Text = "Cooperado: " + _MarcasUsuario.Nombre_Cooperado;
            Cooperado.PlantaINFO.Text = "Planta: "+_MarcasUsuario.Nombre_Planta;
            Cooperado.RutInfo.Text = "R.U.T: "+_MarcasUsuario.RUT_Cooperado;
            Cooperado.LecturaText.Text = _MarcasUsuario.Intento.ToString();
            Cooperado.TicketText.Text = _MarcasUsuario.NUM_TICKET.ToString();
            TicketText.Text = _MarcasUsuario.NUM_TICKET.ToString();
            SQliteManager.CambiarEstado_Data(2, _MarcasUsuario);
        }

        private int ConfirmarPlanta()
        {
            return Convert.ToInt32(File.ReadAllText(ConfigPath).Split(';')[0]);
        }

        #region Configuración del COM
        private void ConfigurarSerial()
        {
            PuertoSerial.PortName = Properties.Settings.Default.NombrePuerto;
            PuertoSerial.BaudRate = Properties.Settings.Default.BaudRate;
            PuertoSerial.DataBits = Properties.Settings.Default.Databits;
            PuertoSerial.Parity = ObtenerParidad(Properties.Settings.Default.Paridad);
        }
        private Parity ObtenerParidad(string _paridadEnArchivo)
        {
            switch (_paridadEnArchivo)
            {
                case "Even":
                    return Parity.Even;
                case "None":
                    return Parity.None;
            }
            return Parity.None;
        }

        private void AccesoForm(string Data)
        {
            try
            {
                strBufferIn = Data;
                //Aqui Limpiamos el texto y lo seteamos.
                //Cooperado.GradoText.Text = strBufferIn;
                _MarcasUsuario.Grado = Convert.ToDecimal(strBufferIn);
            }
            catch (Exception)
            {
                _MarcasUsuario.Grado = 0;
                Cooperado.GradoText.Text = "00,0";
            }
        }

        private void AccesoInterrupcion(string Data)
        {
            try
            {
                DelegadoAcceso var_Delegado = new DelegadoAcceso(AccesoForm);
                object[] arg = { Data };
                base.Invoke(var_Delegado, arg);
            }
            catch (Exception)
            {
                EscribirEnLog("ERROR DE HILOS, EJECUTANDO CODIGO ESPECIAL ...");
            }
        }

        #endregion

        private void EscribirEnLog(string Texto)
        {
            string[] NuevaLinea = new string[] { DateTime.Now.ToString() + " " + Texto };
            File.AppendAllLines(PathLog, NuevaLinea);
        }

        private void Load_Refracto(object sender, EventArgs e)
        {
            Setting_Monitores();
        }

        private void SetAutoRun()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run",true);
            rk.SetValue("RefractoPLC",Application.ExecutablePath);
        }

        void Setting_Monitores()
        {
            Rectangle bounds;
            if (screens.Length > 1)
            {
                bounds = screens[1].Bounds;
                Cooperado.SetBounds(bounds.X, bounds.Y, 1200, 1200);
                Cooperado.StartPosition = FormStartPosition.Manual;
                Cooperado.Show();
                MaximizeBox = false;
                MinimizeBox = false;
                Cooperado.WindowState = FormWindowState.Maximized;
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                bounds = screens[0].Bounds;
                Cooperado.SetBounds(bounds.X,bounds.Y,bounds.Width,bounds.Height);
                Cooperado.WindowState = FormWindowState.Minimized;
            }
                
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
            if (ValorObtenido.CompareTo('G') == 0)
            {
                _observador.estadoActual = EstadoPLC.G;
                EstadoText.Text = "VALVULA DE SALIDA ESPERANDO VACIADO";
                CapturarButton.Enabled = false;
                MedicionButton.Enabled = false;
                RepetirButton.Enabled = false;
                GuardarButton.Enabled = false;
            }
            if (ValorObtenido.CompareTo('H') == 0)
            {
                _observador.estadoActual = EstadoPLC.H;
                EstadoText.Text = "APERTURA Y CIERRE DE VALVULAS DE LAVADO";
                CapturarButton.Enabled = false;
                MedicionButton.Enabled = false;
                RepetirButton.Enabled = false;
                GuardarButton.Enabled = false;
            }
            if (File.ReadAllText(@"C:/ROMANA/REFRACTO/RECEPCION.txt").CompareTo("ERROR") == 0)
            {
                _observador.estadoActual = EstadoPLC.ERROR;
                EstadoText.Text = "ERROR DE PLC.";
                CapturarButton.Enabled = false;
                MedicionButton.Enabled = false;
                RepetirButton.Enabled = false;
                GuardarButton.Enabled = false;
            }
        }

        private void AbortarClick(object sender, EventArgs e)
        {
            if (_observador.estadoActual == EstadoPLC.A && _MarcasUsuario.Intento == 1) {
                DialogResult resultado = MessageBox.Show("¿Estás seguro de querer anular la toma de grado ?. " +
                        "Deberás reiniciar el proceso desde la página web.", "Anular Acción",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
                if (resultado == DialogResult.OK)
                {
                    _MarcasUsuario.Clear_Session();
                    Limpiar_UI();
                    SQliteManager.CambiarEstado_App(4);
                    OracleManager.InsertarEstado_Excepcion(4, _MarcasUsuario.Id_Planta);
                }
            }
            else
            {
                MessageBox.Show("NO PUEDES CANCELAR UNA VEZ INICIADO EL PROCESO.","ERROR",
                    MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
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
                    SQliteManager.CambiarEstado_App(3);
                    PuertoSerial.Close();
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
            if (_MarcasUsuario.Grado > 0) {
                _observador.LeerArchivoMediciones(@"C:/ROMANA/REFRACTO/mediciones.txt", _MarcasUsuario);
                LlenarGrilla();
                
                CapturarButton.Enabled = false;
                RepetirButton.Enabled = true;
                if (_MarcasUsuario.Intento >= 3)
                {
                    CapturarButton.Enabled = false;
                    RepetirButton.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("EL TOMA GRADO PUEDE ESTAR AÚN TRABAJANDO O EL GRADO ES INVALIDO.",
                    "ERROR",MessageBoxButtons.OK,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void GuardarClick(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show("¿DESEA COMPLETA EL PROCESO DE TOMA DE GRADO?, " +
                "ESTE PROCESO NO PUEDE DESHACERSE.","ALERTA",MessageBoxButtons.YesNo,MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
            if (confirmacion == DialogResult.Yes) {
                OracleManager.VerificarIntegridadBaseDeDatos(_MarcasUsuario.Id_Planta);
                InsertarEnBasesDeDatos(_MarcasUsuario);
                _MarcasUsuario.Clear_Session();
                _observador.Intentos_Session = 0;
                _observador.EscribirPalabra_Archivo('F', @"C:/ROMANA/REFRACTO/ENVIO.txt");
                Limpiar_UI();
                MessageBox.Show("Los datos se han guardado con exito en la base de datos.", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                SQliteManager.CambiarEstado_App(3);
                //Thread.Sleep(7000);
                //SQliteManager.CambiarEstado_App(4);
                GuardarButton.Enabled = false;
            }
        }

        private void Limpiar_UI()
        {
            #region Area de Cooperado
            Cooperado.TicketText.Text = "0";
            Cooperado.LecturaText.Text = "0";
            Cooperado.TemperaturaText.Text = "0.0";
            Cooperado.VolumenText.Text = "0.0";
            Cooperado.GradoText.Text = "00,0";
            Cooperado.RutInfo.Text = "RUT: No hay Información";
            //Cooperado.PlantaINFO.Text = "Planta: No hay Información";
            Cooperado.CooperadoINFO.Text = "Cooperado: No hay Información";
            #endregion

            #region Area de Trabajador
            LecturaText.Text = "1";
            TicketText.Text = "0";
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
            DialogResult resultado = MessageBox.Show("Esta acción anulará los valores anteriores y deberá comenzar el proceso de nuevo,¿Está seguro que desea continuar?",
                "Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            if (resultado == DialogResult.Yes)
            {
                OracleManager.VerificarIntegridadBaseDeDatos(_MarcasUsuario.Id_Planta);
                if (_MarcasUsuario.Intento <= 3)
                {
                    InsertarEnBasesDeDatosRepeticion(_MarcasUsuario);
                    Limpiar_Repeticion();
                    _MarcasUsuario.Intento++;
                    Cooperado.LecturaText.Text = _MarcasUsuario.Intento.ToString();
                    LecturaText.Text = _MarcasUsuario.Intento.ToString();
                    _observador.EscribirPalabra_Archivo('R', @"C:/ROMANA/REFRACTO/ENVIO.txt");
                    //MessageBox.Show("Valores cargados por default.", "Nueva Repetición",
                    //    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MedicionButton.Enabled = false;
                    CapturarButton.Enabled = false;
                    RepetirButton.Enabled = false;
                }
            }
            
        }

        private void Limpiar_Repeticion()
        {
            Cooperado.GradoText.Text = "00,0";
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

        private void InsertarEnBasesDeDatosRepeticion(Session _userSession)
        {
            _MarcasUsuario.Hora = DateTime.Now.ToString("HH:mm:ss");
            _MarcasUsuario.Fecha = DateTime.Now.ToString("dd-MM-yyyy");
            ScreenManager.TomarPantallazo(_userSession);
            SQliteManager.InsertarDatos(_userSession);
            OracleManager.InsertarFotoRecepcion(_userSession);
        }

        private void Consultor_Tick(object sender, EventArgs e)
        {
            if (SQliteManager.DebeEjecutarse())
            {
                Configurar_Sesion();
                if (_MarcasUsuario.NUM_TICKET > 0) {
                    BringToFront();
                    OracleManager.VerificarIntegridadBaseDeDatos(_MarcasUsuario.Id_Planta);
                    this.WindowState = FormWindowState.Maximized;
                    TopMost = true;
                    SQliteManager.CambiarEstado_App(2);
                    SQliteManager.EliminarRegistro(_MarcasUsuario.Id_RecepcionUva);
                }
                else
                {
                    SQliteManager.CambiarEstado_App(3);
                    Limpiar_UI();
                    _MarcasUsuario.Clear_Session();
                    WindowState = FormWindowState.Minimized;
                    MessageBox.Show("Hubo un problema de comunicación entre la página y la aplicación. " +
                        "Por favor reinicie el proceso desde el ERP.","Información",MessageBoxButtons.OK,
                        MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            if(SQliteManager.DebeMinimizarse())
            {
                Limpiar_UI();
                _MarcasUsuario.Clear_Session();
                WindowState = FormWindowState.Minimized;
            }
            if (SQliteManager.TrabajoEnProceso())
            {
                if (WindowState != FormWindowState.Minimized)
                {
                    WindowState = FormWindowState.Maximized;
                }
                if (Cooperado.WindowState != FormWindowState.Maximized)
                {
                    Cooperado.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {

                Cooperado.BringToFront();
            }
        }

        private void DataRecibida(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (_MarcasUsuario.NUM_TICKET > 0) {
                try
                {
                    _MarcasUsuario.Id_Planta = ConfirmarPlanta();
                    switch (_MarcasUsuario.Id_Planta)
                    {
                        //Salamanca - Maseli
                        case 2:
                            var Resultado = PuertoSerial.ReadLine();
                            Resultado = Resultado.Substring(0, 5);
                            Resultado = Resultado.Replace(".", ",");
                            //Resultado = Resultado.Replace("F","").Replace("M","");
                            PuertoSerial.DiscardInBuffer();
                            AccesoInterrupcion(Resultado);
                            break;
                        //Sotaqui
                        case 3:
                            var Sotaqui = PuertoSerial.ReadExisting();
                            if (Sotaqui.Contains("F") && Sotaqui.Contains("M")) {
                                Sotaqui = new string(Sotaqui.Take(5).ToArray()).Replace("F", "").Replace("M", "").Replace(".",",");
                                PuertoSerial.DiscardInBuffer();
                                AccesoInterrupcion(Sotaqui);
                                break;
                            }
                            else
                            {
                                PuertoSerial.DiscardInBuffer();
                                AccesoInterrupcion("00,0");
                                break;
                            }
                        case 10:
                            var Punitaqui = PuertoSerial.ReadLine();
                            EscribirEnLog(Punitaqui);
                            Punitaqui = Punitaqui.ToLower();
                            EscribirEnLog(Punitaqui);
                            //BRIX: %VOL
                            Punitaqui = Punitaqui.Substring(5,Punitaqui.IndexOf("%"));
                            EscribirEnLog(Punitaqui);
                            Punitaqui = Punitaqui.Replace("brix;", "").Replace("%","").Replace("vol","").Replace(" ","");
                            EscribirEnLog(Punitaqui);
                            Punitaqui = Punitaqui.Replace(".",",");
                            EscribirEnLog(Punitaqui);
                            PuertoSerial.DiscardInBuffer();
                            AccesoInterrupcion(Punitaqui);
                            break;
                        default:
                            PuertoSerial.DiscardInBuffer();
                            AccesoInterrupcion("00,0");
                            break;
                    }
                }
                catch (Exception _error)
                {
                    EscribirEnLog("Error en la recepción de data. Error: " + _error.Message);
                }
            }
            else
            {
                PuertoSerial.DiscardInBuffer();
                PuertoSerial.DiscardOutBuffer();
                AccesoInterrupcion("00,0");
            }
        }

        private void ErrorRecibido(object sender, SerialErrorReceivedEventArgs e)
        {
            //MessageBox.Show("Error recibido desde COM: "+e.ToString(),"Error",
            //    MessageBoxButtons.OK,MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,
            //    MessageBoxOptions.DefaultDesktopOnly);
            EscribirEnLog("ErrorRecibido desde COM: "+e.ToString());
        }
    }
}
