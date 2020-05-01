using Oracle.ManagedDataAccess.Client;
using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace Romana_AppVendimia.Modelo
{
    public static class OracleManager
    {
        private readonly static string ConexionString = "Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.16.236)(PORT = 1521))) " +
                                "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = orcl.capel.cl))); User Id = PROD_ADM; " +
                                "Password = PROD1530;";
        //private readonly static string ConexionString = "Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.16.240)(PORT = 1521))) " +
        //                        "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = desa.capel.cl))); User Id = usr_prod; " +
        //                        "Password = usrprod;";

        private readonly static string PathLog = @"C:/ROMANA/REFRACTO/LOG/log.txt";

        public static void SetConfiguracionDePuerto(SerialPort puertoSerial,int ID_Planta, int TipoProceso)
        {
            try
            {
                using (var conexion = new OracleConnection(ConexionString))
                {
                    conexion.Open();
                    EscribirEnLog("Conexión a Oracle Exitosa.");
                    using (var command = conexion.CreateCommand())
                    {
                        command.CommandText = "Select Nombre_Puerto,byte, paridad,databits,observacion " +
                            "from cooper_adm.parametro where ID_Planta = "+ID_Planta+" and ID_Tipo ="+TipoProceso;
                        command.CommandType = System.Data.CommandType.Text;
                        var dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            Properties.Settings.Default.NombrePuerto = dr.GetString(0);
                            Properties.Settings.Default.BaudRate = dr.GetInt32(1);
                            Properties.Settings.Default.Paridad = dr.GetString(2);
                            Properties.Settings.Default.Databits = dr.GetInt32(3);
                            Properties.Settings.Default.Valor_Anexado = dr.GetString(4);
                            
                        }
                        Properties.Settings.Default.Save();
                        conexion.Close();
                    }
                }

                    
            }
            catch (Exception e)
            {
                EscribirEnLog("Error al configurar el puerto. "+e.Message);
            }
        }

        private static void EscribirEnLog(string Texto)
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

        public static void InsertarDatosEnPasarela(Session _userSession)
        {
            try
            {
                using (var conexion = new OracleConnection(ConexionString))
                {
                    conexion.Open();
                    var comando = "insert into cooper_adm.peso_romana" +
                                    "(id_peso_romana,Grado,Estado,Fecha,ID_Planta,Tipo_Proceso,Temperatura,Volumen) " +
                                    "VALUES(cooper_adm.seq_peso_romana.nextval,:grado,0,:fecha,:planta,:proceso,:temperatura,:volumen)";
                    using (var cmd = new OracleCommand(comando, conexion))
                    {
                        OracleParameter[] parametros = new OracleParameter[]
                        {
                            new OracleParameter("grado",_userSession.Grado),
                            new OracleParameter("fecha",_userSession.Fecha),
                            new OracleParameter("planta",_userSession.Id_Planta),
                            new OracleParameter("proceso",_userSession.Tipo_Proceso),
                            new OracleParameter("temperatura",_userSession.Temperatura),
                            new OracleParameter("volumen",_userSession.Volumen)
                        };
                        cmd.Parameters.AddRange(parametros);
                        cmd.ExecuteNonQuery();
                    }
                    conexion.Close();
                    EscribirEnLog("Datos insertados en pasarela correctamente.");

                }
            }
            catch (Exception _error)
            {
                MessageBox.Show("Error al Insertar los DATOS en BD Oracle","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
                EscribirEnLog("Error al insertar en Oracle. Error: "+_error);
            }
        }

        public static void InsertarFotoRecepcion(Session _userSession)
        {
            try
            {
                using (var conexion = new OracleConnection(ConexionString))
                {
                    conexion.Open();
                    var comando = "insert into cooper_adm.recepcion_uva_foto " +
                                    "(ID_RECEPCION_UVA,NUM_TICKET_ATENCION,OBSERVACION,IMAGEN,LECTURA,FECHA,COSECHA,ID_PLANTA,TIPO_CONTROL) " +
                                    "VALUES(:recepcion,:ticket,:observacion,:imagen,:lectura,:fecha,:cosecha,:planta,:control)";
                    using (var cmd = new OracleCommand(comando, conexion))
                    {
                        OracleParameter[] parametros = new OracleParameter[]
                        {
                            new OracleParameter("recepcion",_userSession.Id_RecepcionUva),
                            new OracleParameter("ticket",_userSession.NUM_TICKET),
                            new OracleParameter("observacion","No hay problemas."),
                            new OracleParameter("imagen",_userSession.Imagen),
                            new OracleParameter("lectura",_userSession.Intento),
                            new OracleParameter("fecha",_userSession.Fecha),
                            new OracleParameter("cosecha",DateTime.Now.Year),
                            new OracleParameter("planta",_userSession.Id_Planta),
                            new OracleParameter("control",2)
                        };
                        cmd.Parameters.AddRange(parametros);
                        cmd.ExecuteNonQuery();
                    }
                    conexion.Clone();
                    EscribirEnLog("Imagen Insertada correctamente en BD.");
                }
            }
            catch (Exception _error)
            {
                MessageBox.Show("Error al Insertar Pantallazo en Base de Datos Local","Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,MessageBoxOptions.DefaultDesktopOnly);
                EscribirEnLog("Error al subir foto. Error: "+_error.Message);
            }
        }

        public static void VerificarIntegridadBaseDeDatos(int _idPlanta)
        {
            try
            {
                using (var Conexion = new OracleConnection(ConexionString))
                {
                    Conexion.Open();
                    using (var Command = Conexion.CreateCommand())
                    {
                        Command.CommandText = "Select ID_PESO_ROMANA from cooper_adm.peso_romana where Estado = 0 and " +
                            "ID_PLANTA = " + _idPlanta + " and TIPO_PROCESO = 1";
                        Command.CommandType = System.Data.CommandType.Text;
                        var dr = Command.ExecuteReader();
                        while (dr.Read())
                        {
                            using (var Comando = Conexion.CreateCommand())
                            {
                                Comando.CommandText = "UPDATE cooper_adm.peso_romana SET ESTADO = 1, CADENA = 'ERROR DE CAPTURA - PLC' " +
                                    "WHERE ID_PESO_ROMANA = " + dr.GetInt32(0);
                                Comando.ExecuteNonQuery();
                            }
                        }
                    }
                    Conexion.Close();
                }
            }
            catch (OracleException e)
            {
                EscribirEnLog("Error en la Consulta Oracle. Error : " + e.Message);
            }
        }

        public static void InsertarEstado_Excepcion(int _estadoNuevo,int _planta)
        {
            try
            {
                using (var conexion = new OracleConnection(ConexionString))
                {
                    conexion.Open();
                    var comando = "insert into cooper_adm.peso_romana" +
                                    "(id_peso_romana,Estado,Fecha,ID_Planta,Tipo_Proceso) " +
                                    "VALUES(cooper_adm.seq_peso_romana.nextval,:estado,sysdate,:planta,:proceso)";
                    using (var cmd = new OracleCommand(comando, conexion))
                    {
                        OracleParameter[] parametros = new OracleParameter[]
                        {
                            new OracleParameter("estado",_estadoNuevo),
                            new OracleParameter("planta",_planta),
                            new OracleParameter("proceso",1),
                        };
                        cmd.Parameters.AddRange(parametros);
                        cmd.ExecuteNonQuery();
                    }
                    conexion.Close();
                    EscribirEnLog("Datos insertados en pasarela correctamente.");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al Insertar los DATOS en BD Oracle", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
