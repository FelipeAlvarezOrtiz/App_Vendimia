using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romana_AppVendimia.Modelo
{
    public class SerialPortManager
    {
        private SerialPort _puertoSerial;
        public Decimal GradoLeido = 0;
        private string valorDuro = string.Empty;
        private readonly static string PathLog = @"C:/ROMANA/REFRACTO/LOG/log.txt";

        public SerialPortManager()
        {
            _puertoSerial = new SerialPort()
            {
                PortName = Properties.Settings.Default.NombrePuerto,
                BaudRate = Properties.Settings.Default.BaudRate,
                DataBits = Properties.Settings.Default.Databits,
                Parity = ObtenerParidad(Properties.Settings.Default.Paridad),
            };
            //_puertoSerial.DataReceived += new SerialDataReceivedEventHandler(RefractoDataReceived);
        }

        public void SetearGrado(Session _userSession)
        {
            ObtenerGrado();
            _userSession.Grado = Decimal.Parse(LimpiarValor());
            _puertoSerial.Close();
        }

        private void RefractoDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            valorDuro = sp.ReadExisting();
        }

        public string LimpiarValor()
        {
            return new string(valorDuro.Take(5).ToArray()).Replace("F", "").Replace("M", "");
        }

        public void ObtenerGrado()
        {
            try
            {
                _puertoSerial.Open();
                _puertoSerial.DataReceived += new SerialDataReceivedEventHandler(RefractoDataReceived);
            }
            catch (Exception e)
            {
                EscribirEnLog("Error obteniendo el grado desde Puerto Serial."+e.Message);
            }
        }

        private void EscribirEnLog(string Texto)
        {
            string[] NuevaLinea = new string[] { DateTime.Now.ToString() + " " + Texto };
            File.AppendAllLines(PathLog, NuevaLinea);
        }

        private Parity ObtenerParidad(string _paridadEnArchivo)
        {
            switch (_paridadEnArchivo)
            {
                case "e":
                    return Parity.Even;
                case "None":
                    return Parity.None;
            }
            return Parity.None;
        }
    }
}
