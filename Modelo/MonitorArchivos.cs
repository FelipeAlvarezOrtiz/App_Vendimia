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
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class MonitorArchivos
    {
        private readonly Vista_Cooperado _cooperado;
        private readonly Trabajador_Vista _trabajador;
        private readonly string PathParaMonitoreo = @"C:/ROMANA/REFRACTO/";
        private readonly string PathLog = @"C:/ROMANA/REFRACTO/LOG/log.txt";
        private readonly FileSystemWatcher _observador = new FileSystemWatcher();
        private EstadoPLC estadoActual = EstadoPLC.Z;
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
            if (string.Compare(Path.GetFileName(_event.FullPath),"RECEPCION.TXT") == 0)
            {
                _trabajador.IniciarButton.Enabled = false;
                char _estadoPLC = LeerPrimerChar_Archivo(_event.FullPath);
                if (_estadoPLC.CompareTo('A') == 0 && estadoActual != EstadoPLC.A)
                {
                    estadoActual = EstadoPLC.A;
                    _trabajador.EstadoText.Text = "EN ESPERA COMPLETAR NIVEL (5 LTS.)";
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
                    _trabajador.EstadoText.Text = "VALVULA SALIDA SE CIERRA PARA COMPLETAR 7 LTS. MEDICIÓN.";
                    _trabajador.CapturarButton.Enabled = false;
                    _trabajador.MedicionButton.Enabled = false;
                    _trabajador.RepetirButton.Enabled = false;
                    _trabajador.GuardarButton.Enabled = false;
                }
                if (_estadoPLC.CompareTo('D') == 0 && estadoActual != EstadoPLC.D)
                {
                    estadoActual = EstadoPLC.D;
                    _trabajador.EstadoText.Text = "SISTEMA LISTO PARA ADQUISICIÓN CONTIENE 7 LTS. O MÁS";
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
                    _trabajador.RepetirButton.Enabled = true;
                    _trabajador.GuardarButton.Enabled = false;
                }
            }

        }

        private void OnRenamed(object source, FileSystemEventArgs _event)
        {

        }

        private void EscribirEnLog(string Texto)
        {
            string[] NuevaLinea = new string[] { DateTime.Now.ToString() + " " + Texto };
            File.AppendAllLines(PathLog,NuevaLinea);
        }

        private char LeerPrimerChar_Archivo(string Path)
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

        public void Run()
        {
            _observador.EnableRaisingEvents = true;
            EscribirEnLog("Monitor de Archivos activado.");
        }

        public void LeerArchivoMediciones(string Path)
        {
            try
            {
                DataMediciones.Clear();
                Intentos_Session++;
                var Data = File.ReadAllText(Path).Split(';');
                _cooperado.TemperaturaText.Text = Data[1];
                _cooperado.VolumenText.Text = Data[2];
                _cooperado.TicketText.Text = Intentos_Session.ToString();
                //Falta Ticket

                DataMediciones.Add(Data[1]);
                DataMediciones.Add(Data[2]);
                DataMediciones.Add(ObtenerTipoProcedimiento((int)Convert.ToInt32(Data[3])));
                DataMediciones.Add(Intentos_Session.ToString());

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
    }

}