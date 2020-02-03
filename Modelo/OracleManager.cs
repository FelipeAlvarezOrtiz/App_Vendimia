﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
