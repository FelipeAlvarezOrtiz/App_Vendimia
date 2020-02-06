using Oracle.ManagedDataAccess.Client;
using System;
using System.IO;

namespace Romana_AppVendimia.Modelo
{
    public static class OracleManager
    {
        private readonly static string ConexionString = "Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.16.240)(PORT = 1521))) " +
                                "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = desa.capel.cl))); User Id = usr_prod; " +
                                "Password = usrprod;";

        private readonly static string PathLog = @"C:/ROMANA/REFRACTO/LOG/log.txt";

        public static void SetConfiguracionDePuerto(int ID_Planta, int TipoProceso)
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
                EscribirEnLog(e.Message);
            }
        }

        private static void EscribirEnLog(string Texto)
        {
            string[] NuevaLinea = new string[] { DateTime.Now.ToString() + " " + Texto };
            File.AppendAllLines(PathLog, NuevaLinea);
        }

        public static void InsertarDatosEnPasarela(Session _userSession)
        {
            using (var conexion = new OracleConnection(ConexionString))
            {
                conexion.Open();
                var comando = "insert into cooper_adm.peso_romana" +
                                "(id_peso_romana,Grado,Estado,Fecha,ID_Planta,Tipo_Proceso,Temperatura,Volumen) " +
                                "VALUES(cooper_adm.seq_peso_romana.nextval,:grado,1,:fecha,:planta,:proceso,:temperatura,:volumen)";
                using (var cmd = new OracleCommand(comando,conexion))
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

        public static void InsertarFotoRecepcion(Session _userSession)
        {
            using (var conexion = new OracleConnection(ConexionString))
            {
                conexion.Open();
                var comando = "insert into cooper_adm.recepcion_uva_foto " +
                                "(ID_RECEPCION_UVA,OBSERVACION,IMAGEN,LECTURA,FECHA) " +
                                "VALUES(:id_recepcion,:observacion,:imagen,:lectura,:fecha)";
                using (var cmd = new OracleCommand(comando,conexion))
                {
                    OracleParameter[] parametros = new OracleParameter[]
                    {
                        new OracleParameter("id_recepcion",_userSession.Id_RecepcionUva),
                        new OracleParameter("observacion","No hay problemas."),
                        new OracleParameter("imagen",_userSession.Imagen),
                        new OracleParameter("lectura",_userSession.Intento),
                        new OracleParameter("fecha",_userSession.Fecha)
                    };
                    cmd.Parameters.AddRange(parametros);
                    cmd.ExecuteNonQuery();
                }
                conexion.Clone();
                EscribirEnLog("Imagen Insertada correctamente en BD.");
            }
        }

    }
}
