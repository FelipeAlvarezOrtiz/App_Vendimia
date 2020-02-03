using System;
using System.Data.SQLite;
using System.IO;

namespace Romana_AppVendimia.Modelo
{
    static class SQliteManager
    {
        private readonly static string CreacionDB = "";

        public static string NombreDB = "Refracto.db";
        public static string FullPathDB = @"C:/ROMANA/DB/" + NombreDB;

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
                using (var Conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
                using (var Command = new SQLiteCommand(CreacionDB, Conexion))
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
                //Estado_Data = 3 Guardada
                using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
                using (var command = new SQLiteCommand("INSERT INTO Refracto_Info(ID_Recepcion_Uva, ID_Ticket, Nombre_Cooperado, Nombre_Planta, Lectura, " +
                    "Temperatura, Volumen, Grado, Estado_Data, ID_planta, Foto, Fecha, Hora) VALUES" +
                    "(@Recepcion,@Ticket,@NombreCooperado, @NombrePlanta,@Intento,@Tempe,@Volume,@Grado,3,@IdPlanta,@Foto,@Fecha,@Hora)", conexion))
                {
                    conexion.Open();
                    command.Parameters.Add("@Recepcion", System.Data.DbType.Int32).Value = _session.Id_RecepcionUva;
                    command.Parameters.Add("@Ticket", System.Data.DbType.Int32).Value = _session.Id_Ticket;
                    command.Parameters.Add("@NombreCooperado", System.Data.DbType.String).Value = _session.Nombre_Cooperado;
                    command.Parameters.Add("@NombrePlanta", System.Data.DbType.String).Value = _session.Nombre_Planta;
                    command.Parameters.Add("@Intento", System.Data.DbType.Int32).Value = _session.Intento;
                    command.Parameters.Add("@Tempe", System.Data.DbType.Decimal).Value = _session.Temperatura;
                    command.Parameters.Add("@Volume", System.Data.DbType.Decimal).Value = _session.Volumen;
                    command.Parameters.Add("@Grado", System.Data.DbType.Decimal).Value = _session.Grado;
                    command.Parameters.Add("@IdPlanta", System.Data.DbType.Int32).Value = _session.Id_Planta;
                    command.Parameters.Add("@Foto", System.Data.DbType.Binary, 20).Value = _session.Imagen;
                    command.Parameters.Add("@Fecha", System.Data.DbType.String).Value = _session.Fecha;
                    command.Parameters.Add("@Hora", System.Data.DbType.String).Value = _session.Hora;

                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
            catch (Exception e)
            {
                EscribirEnLog("Error al insertar en la base de datos local. " + e.Message);
            }
        }

        public static bool DebeEjecutarse()
        {
            using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
            using (var command = new SQLiteCommand("Select Estado from Estado_App where Estado = 1;", conexion))
            {
                try
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        conexion.Close();
                        return true;
                    }
                    else
                    {
                        conexion.Close();
                        return false;
                    }
                }
                catch (Exception e)
                {
                    EscribirEnLog("Error al intentar consultar en Base de Datos Local." + e.Message);
                    return false;
                }
            }
        }

        public static void Configurar_Session(Session _userSession)
        {
            using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
            using (var command = new SQLiteCommand("Select ID_Recepcion_UVA, ID_Ticket, Nombre_Cooperado, Nombre_Planta from " +
                                                    "Refracto_Info where Estado_Data= 1", conexion))
            {
                try
                {
                    conexion.Open();
                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            _userSession.Id_RecepcionUva = rdr.GetInt32(0);
                            _userSession.Id_Ticket = rdr.GetInt32(1);
                            _userSession.Nombre_Cooperado = rdr.GetString(2);
                            _userSession.Nombre_Planta = rdr.GetString(3);
                        }
                    }
                    conexion.Close();
                }
                catch (Exception e)
                {
                    EscribirEnLog("Error al leer la Session en base de datos local. " + e.Message);
                }
            }

        }

        public static void CambiarEstado_App(int Valor)
        {
            using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
            using (var command = new SQLiteCommand("Update Estado_App SET Estado = @Status;", conexion))
            {
                try
                {
                    conexion.Open();
                    command.Parameters.Add("@Status",System.Data.DbType.Int32).Value = Valor;
                    command.ExecuteNonQuery();
                    EscribirEnLog("Estado de la aplicación modificado a "+Valor+".");
                    conexion.Close();
                }
                catch (Exception)
                {
                    EscribirEnLog("Error al cambiar el Estado de la Aplicación en BD Local.");
                }
            }

        }
    }
}
