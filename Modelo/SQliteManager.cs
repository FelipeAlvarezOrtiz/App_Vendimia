using System;
using System.Data.SQLite;
using System.IO;

namespace Romana_AppVendimia.Modelo
{
    static class SQliteManager
    {
        private readonly static string CreacionDB = "";

        public static string NombreDB = "Romana.db";
        public static string FullPathDB = @"C:/ROMANA/DB/"+NombreDB;

        public static string PathLog = @"C:/ROMANA/REFRACTO/LOG/log.txt";

        public static void EscribirEnLog(string Texto)
        {
            string[] NuevaLinea = new string[] { DateTime.Now.ToString() + " " + Texto };
            File.AppendAllLines(PathLog, NuevaLinea);
        }

        public static bool CheckDataBase()
        {
            if (File.Exists(FullPathDB))
            {
                Console.WriteLine();
                return true;
            }
            else
            {
                using (var Conexion = new SQLiteConnection("Data Source="+FullPathDB+";Version=3"))
                using (var Command = new SQLiteCommand(CreacionDB,Conexion))
                {
                    try
                    {
                        Conexion.Open();
                        Command.ExecuteNonQuery();
                        if (Conexion.State == System.Data.ConnectionState.Open)
                        {
                            EscribirEnLog("La base de datos local no existia, pero se ha creado correctamente.");
                        }
                        else
                        {
                            EscribirEnLog("Error al crear la base de datos local.");
                        }
                        Conexion.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        EscribirEnLog(e.Message);
                        return false;
                    }
                }
            }
        }

        public static void InsertarDatos(Session _session)
        {
            try
            {
                using (var conexion = new SQLiteConnection("DataSource=" + FullPathDB + ";Version=3"))
                using (var command = new SQLiteCommand("INSERT INTO ", conexion))
                {
                    conexion.Open();
                    command.Parameters.Add("@ID_Ticket", System.Data.DbType.Int32).Value = _session.Id_Ticket;
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
            catch(Exception e)
            {
                EscribirEnLog("Error al insertar en la base de datos local. "+e.Message);
            }
        }

    }
}
