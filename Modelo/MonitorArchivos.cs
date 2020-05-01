using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;

namespace Romana_AppVendimia.Modelo
{
    public enum EstadoPLC
    {
        Z = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
        F = 6,
        G = 7,
        H = 8,
        ERROR = 9,
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class MonitorArchivos
    {
        private readonly Vista_Cooperado _cooperado;
        private readonly Trabajador_Vista _trabajador;
        private readonly string PathParaMonitoreo = @"C:/ROMANA/REFRACTO";
        private readonly string PathLog = @"C:/ROMANA/REFRACTO/LOG/log.txt";
        private readonly string PathConfig = @"C:/ROMANA/config.txt";
        private readonly FileSystemWatcher _observador = new FileSystemWatcher();
        public EstadoPLC estadoActual = EstadoPLC.Z;
        public int Intentos_Session = 0;
        public List<string> DataMediciones = new List<string>();

        public MonitorArchivos(Trabajador_Vista _trabajadorVista,Vista_Cooperado _cooperadoVista)
        {
            _cooperado = _cooperadoVista;
            _trabajador = _trabajadorVista;
            ConfigurarFileWatcher();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void ConfigurarFileWatcher()
        {
            _observador.Path = PathParaMonitoreo;

            _observador.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.Size
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

            _observador.Filter = "*.txt";

            _observador.Changed += OnChanged;
            _observador.Renamed += OnRenamed;

            _observador.EnableRaisingEvents = false;

        }

        private void OnChanged(object source, FileSystemEventArgs _event)
        {
            if (string.Compare(Path.GetFileName(_event.FullPath),"RECEPCION.txt") == 0)
            {
                _trabajador.IniciarButton.Enabled = false;
                char _estadoPLC = LeerPrimerChar_Archivo(_event.FullPath);
                if (_estadoPLC.CompareTo('A') == 0 && estadoActual != EstadoPLC.A)
                {
                    estadoActual = EstadoPLC.A;
                    _trabajador.EstadoText.Text = "EN ESPERA COMPLETAR NIVEL ("+ ObtenerLitrosPrimerEventoDePlanta(ConfirmarPlanta())+")";
                    _trabajador.CapturarButton.Enabled = false;
                    _trabajador.MedicionButton.Enabled = false;
                    _trabajador.RepetirButton.Enabled = false;
                    _trabajador.GuardarButton.Enabled = false;
                }
                if (_estadoPLC.CompareTo('B') == 0 && estadoActual != EstadoPLC.B)
                {
                    estadoActual = EstadoPLC.B;
                    _trabajador.EstadoText.Text = "APERTURA VALVULA DE SALIDA PARA COMPLETAR AMBIENTACIÓN.";
                    _trabajador.CapturarButton.Enabled = false;
                    _trabajador.MedicionButton.Enabled = false;
                    _trabajador.RepetirButton.Enabled = false;
                    _trabajador.GuardarButton.Enabled = false;
                }
                if (_estadoPLC.CompareTo('C') == 0 && estadoActual != EstadoPLC.C)
                {
                    estadoActual = EstadoPLC.C;
                    _trabajador.EstadoText.Text = "VALVULA SALIDA SE CIERRA PARA COMPLETAR "+ObtenerLitrosSegundoEventoDePlanta(ConfirmarPlanta())+" MEDICIÓN.";
                    _trabajador.CapturarButton.Enabled = false;
                    _trabajador.MedicionButton.Enabled = false;
                    _trabajador.RepetirButton.Enabled = false;
                    _trabajador.GuardarButton.Enabled = false;
                }
                if (_estadoPLC.CompareTo('D') == 0 && estadoActual != EstadoPLC.D)
                {
                    estadoActual = EstadoPLC.D;
                    _trabajador.EstadoText.Text = "SISTEMA LISTO PARA ADQUISICIÓN CONTIENE " + ObtenerLitrosSegundoEventoDePlanta(ConfirmarPlanta()) + " O MÁS";
                    _trabajador.CapturarButton.Enabled = false;
                    _trabajador.MedicionButton.Enabled = true;
                    _trabajador.RepetirButton.Enabled = false;
                    _trabajador.GuardarButton.Enabled = false;
                }
                if (_estadoPLC.CompareTo('E') == 0 && estadoActual != EstadoPLC.E)
                {
                    estadoActual = EstadoPLC.E;
                    _trabajador.EstadoText.Text = "SISTEMA LISTO PARA ADQUISICIÓN PERO BAJO NIVEL.";
                    _trabajador.CapturarButton.Enabled = false;
                    _trabajador.MedicionButton.Enabled = false;
                    _trabajador.RepetirButton.Enabled = false;
                    _trabajador.GuardarButton.Enabled = false;
                }
                if (_estadoPLC.CompareTo('F') == 0 && estadoActual != EstadoPLC.F)
                {
                    //ESTO TENER OJO, POR SI ESTA CONTINUAMENTE ESCRIBIENDO LETRAS
                    estadoActual = EstadoPLC.F;
                    _trabajador.EstadoText.Text = "SISTEMA LISTO PARA RECIBIR TÉRMINO DE ADQUISICIÓN.";
                    _trabajador.CapturarButton.Enabled = true;
                    _trabajador.MedicionButton.Enabled = false;
                    _trabajador.RepetirButton.Enabled = false;
                    _trabajador.GuardarButton.Enabled = false;
                }
                if (_estadoPLC.CompareTo('G') == 0 && estadoActual != EstadoPLC.G)
                {
                    estadoActual = EstadoPLC.G;
                    _trabajador.EstadoText.Text = "VALVULA DE SALIDA ESPERANDO VACIADO";
                    _trabajador.CapturarButton.Enabled = false;
                    _trabajador.MedicionButton.Enabled = false;
                    _trabajador.RepetirButton.Enabled = false;
                    _trabajador.GuardarButton.Enabled = false;
                }
                if (_estadoPLC.CompareTo('H') == 0 && estadoActual != EstadoPLC.H)
                {
                    estadoActual = EstadoPLC.H;
                    _trabajador.EstadoText.Text = "APERTURA Y CIERRE DE VALVULAS DE LAVADO";
                    _trabajador.CapturarButton.Enabled = false;
                    _trabajador.MedicionButton.Enabled = false;
                    _trabajador.RepetirButton.Enabled = false;
                    _trabajador.GuardarButton.Enabled = false;
                }
            }

        }

        private void OnRenamed(object source, FileSystemEventArgs _event)
        {

        }
        
        public void Stop()
        {
            _observador.EnableRaisingEvents = false;
            _observador.Dispose();
        }

        private void EscribirEnLog(string Texto)
        {
            try
            {
                string[] NuevaLinea = new string[] { DateTime.Now.ToString() + " " + Texto };
                File.AppendAllLines(PathLog, NuevaLinea);
            }
            catch (Exception)
            {

            }
        }

        public char LeerPrimerChar_Archivo(string Path)
        {
            try
            {
                return File.ReadAllText(Path)[0];
            }
            catch (Exception e)
            {
                EscribirEnLog("Error al Leer el primer caracter del Archivo: "+Path);
                EscribirEnLog(e.Message);
                return new char();
            }
        }

        private int ConfirmarPlanta()
        {
            return Convert.ToInt32(File.ReadAllText(PathConfig).Split(';')[0]);
        }

        public void Run()
        {
            _observador.EnableRaisingEvents = true;
            EscribirEnLog("Monitor de Archivos activado.");
        }

        public string ObtenerLitrosPrimerEventoDePlanta(int _idPlanta)
        {
            switch (_idPlanta)
            {
                case 2:
                    return "5 LTS.";
                case 3:
                    return "3 LTS.";
                case 10:
                    return "3 LTS.";
            }
            return "5 LTS.";
        }
        public string ObtenerLitrosSegundoEventoDePlanta(int _idPlanta)
        {
            switch (_idPlanta)
            {
                case 2:
                    return "7 LTS.";
                case 3:
                    return "5 LTS.";
                case 10:
                    return "5 LTS.";
            }
            return "7 LTS.";
        }
        public void EscribirPalabra_Archivo(char Comando, string Path)
        {
            if (File.Exists(Path))
            {
                File.WriteAllLines(Path, new string[] { new string(new char[] { Comando})});
            }
            else
            {
                EscribirEnLog("Error la ruta "+Path+" no existe.");
            }
        }

        public void LeerArchivoMediciones(string Path,Session _sessionActual)
        {
            try
            {
                DataMediciones.Clear();
                Intentos_Session++;
                var Data = File.ReadAllText(Path).Split(';');
                //Falta Ticket
                /*
                DataMediciones.Add(Data[1]);
                DataMediciones.Add(Data[2]);
                DataMediciones.Add(ObtenerTipoProcedimiento((int)Convert.ToInt32(Data[3])));
                DataMediciones.Add(Intentos_Session.ToString());*/
                _sessionActual.Volumen = Convert.ToDecimal(Data[2].Replace(".",","));
                _sessionActual.Temperatura = (Decimal)Convert.ToDecimal(Data[1].Replace(".",","));
                _sessionActual.Operacion = ObtenerTipoProcedimiento(Convert.ToInt32(Data[3]));
                //Este grado debe eliminarse para reemplazar el real.
                //_sessionActual.Grado = Convert.ToDecimal(Data[0].Replace(".",","));
                //_sessionActual.Grado = 
                _sessionActual.Intento = Intentos_Session;
                if (_sessionActual.Temperatura > 30 || _sessionActual.Temperatura <= 2)
                {
                    _sessionActual.Temperatura = 20.5M;
                }
                _cooperado.TemperaturaText.Text = _sessionActual.Temperatura.ToString();
                _cooperado.GradoText.Text = _sessionActual.Grado.ToString();
                _cooperado.VolumenText.Text = _sessionActual.Volumen.ToString();
                _cooperado.TicketText.Text = _sessionActual.NUM_TICKET.ToString();
                _cooperado.LecturaText.Text = _sessionActual.Intento.ToString();
                _cooperado.GradoText.Text = _sessionActual.Grado.ToString();
                
                _trabajador.GuardarButton.Enabled = true;               

            }
            catch (Exception e)
            {
                EscribirEnLog("Error al leer el archivo Mediciones. "+e.Message);
            }
        }

        private string ObtenerTipoProcedimiento(int ID_Procedimiento)
        {
            if (ID_Procedimiento == 1)
            {
                return "Automatico";
            }
            else
            {
                return "Manual";
            }
        }
    
        public void InsertarProceso_Planta(Session _sessionActual)
        {
            if (File.Exists(PathConfig))
            {
                //Primer dato es la Planta, segundo el proceso
                var data = File.ReadAllText(PathConfig).Split(';');
                _sessionActual.Id_Planta = Convert.ToInt32(data[0]);
                _sessionActual.Tipo_Proceso = Convert.ToInt32(data[1]);
            }
            else
            {
                EscribirEnLog("Error al leer el archivo de configuración.");
            }
        }

    }

}