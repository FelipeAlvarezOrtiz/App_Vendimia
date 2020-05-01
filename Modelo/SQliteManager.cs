using System;
using System.Data.SQLite;
using System.IO;

namespace Romana_AppVendimia.Modelo
{
    static class SQliteManager
    {
        #region DB

        private readonly static string CreacionDB = "CREATE TABLE \"Refracto_Info\" ("+
	                                            "\"ID_Recepcion_Uva\"	INTEGER,"+
	                                            "\"NUM_Ticket\"	INTEGER,"+
	                                            "\"Nombre_Cooperado\"	TEXT,"+
	                                            "\"Nombre_Planta\"	TEXT,"+
	                                            "\"Lectura\"	INTEGER,"+
	                                            "\"Temperatura\"	REAL,"+
	                                            "\"Volumen\"	REAL,"+
	                                            "\"Grado\"	REAL,"+
	                                            "\"Estado_Data\"	INTEGER,"+
	                                            "\"ID_Planta\"	INTEGER,"+
	                                            "\"Foto\"	BLOB,"+
	                                            "\"Fecha\"	TEXT,"+
	                                            "\"Hora\"	TEXT,"+
	                                            "\"RUT_Cooperado\"	TEXT,"+
	                                            "\"ID_Cooperado\"	INTEGER,"+
                                                "\"Tipo_Control\"    INTEGER,"+
	                                            "PRIMARY KEY(\"ID_Recepcion_Uva\",\"NUM_TICKET\",\"RUT_Cooperado\",\"Lectura\"));";
        public static string NombreDB = "REFRACTO.db";
        public static string FullPathDB = @"C:/ROMANA/DB/" + NombreDB;

        #endregion

        public static string PathLog = @"C:/ROMANA/REFRACTO/LOG/log.txt";

        public static void EscribirEnLog(string Texto)
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
                //if (_session.Intento > 1)
                //{
                using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
                using (var command = new SQLiteCommand("INSERT INTO LOG_Refracto_Info" +
                    "(ID_RECEPCION_UVA, NUM_TICKET, Nombre_Cooperado, Nombre_Planta, Lectura, " +
                    "Temperatura, Volumen, Grado, Estado_Data, ID_planta, Foto, Fecha, Hora,RUT_Cooperado,ID_Cooperado,TIPO_CONTROL)" +
                    "VALUES(@Recepcion,@Ticket,@NombreCooperado, @NombrePlanta,@Intento," +
                    "@Tempe,@Volume,@Grado,2,@IdPlanta,@Foto,@Fecha,@Hora,@rutCoop,0,2)", conexion))
                {
                    conexion.Open();
                    command.Parameters.Add("@Recepcion", System.Data.DbType.Int32).Value = _session.Id_RecepcionUva;
                    command.Parameters.Add("@Ticket", System.Data.DbType.Int32).Value = _session.NUM_TICKET;
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
                    command.Parameters.Add("@rutCoop", System.Data.DbType.String).Value = _session.RUT_Cooperado;
                    command.Parameters.Add("@idCoop", System.Data.DbType.Int32).Value = _session.ID_Cooperado;

                    command.ExecuteNonQuery();
                    conexion.Close();
                }
                #region CODIGO DESCONTINUADO
                //}
                //else
                //{
                //    using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
                //    using (var command = new SQLiteCommand("UPDATE Refracto_Info SET Lectura = @Intento,Temperatura = @Tempe, Volumen = @Volume, " +
                //        "Grado = @Grade, ID_Planta = @IDPlanta, Foto = @Imagen, Fecha = @Fecha, Hora = @Hora WHERE ID_Recepcion_Uva = @ID_Recepcion and NUM_TICKET = @Ticket", conexion))
                //    {
                //        conexion.Open();

                //        command.Parameters.Add("@Intento", System.Data.DbType.Int32).Value = _session.Intento;
                //        command.Parameters.Add("@Tempe", System.Data.DbType.Decimal).Value = _session.Temperatura;
                //        command.Parameters.Add("@Volume", System.Data.DbType.Decimal).Value = _session.Volumen;
                //        command.Parameters.Add("@Grade", System.Data.DbType.Decimal).Value = _session.Grado;
                //        command.Parameters.Add("@IDPlanta", System.Data.DbType.Int32).Value = _session.Id_Planta;
                //        command.Parameters.Add("@Imagen", System.Data.DbType.Binary, 20).Value = _session.Imagen;
                //        command.Parameters.Add("@Fecha", System.Data.DbType.String).Value = _session.Fecha;
                //        command.Parameters.Add("@Hora", System.Data.DbType.String).Value = _session.Hora;

                //        command.Parameters.Add("@ID_Recepcion", System.Data.DbType.Int32).Value = _session.Id_RecepcionUva;
                //        command.Parameters.Add("@Ticket", System.Data.DbType.Int32).Value = _session.NUM_TICKET;
                //        //command.Parameters.Add("@NombreCooperado", System.Data.DbType.String).Value = _session.Nombre_Cooperado;
                //        //command.Parameters.Add("@NombrePlanta", System.Data.DbType.String).Value = _session.Nombre_Planta;

                //        command.ExecuteNonQuery();
                //        conexion.Close();
                //    }
                //}
                #endregion
            }
            catch (Exception e)
            {
                EscribirEnLog("Error al insertar en la base de datos local. " + e.Message);
            }
        }

        public static bool DebeEjecutarse()
        {
            using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
            using (var command = new SQLiteCommand("Select Estado from ESTADO_APP where Estado = 1;", conexion))
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

        public static bool TrabajoEnProceso()
        {
            using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
            using (var command = new SQLiteCommand("Select Estado from ESTADO_APP where Estado = 2;", conexion))
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
        public static bool DebeMinimizarse()
        {
            using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
            using (var command = new SQLiteCommand("Select Estado from ESTADO_APP where Estado in (0,3,4);", conexion))
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
            using (var command = new SQLiteCommand("Select ID_Recepcion_UVA, NUM_TICKET, Nombre_Cooperado,Lectura, Nombre_Planta,RUT_Cooperado,ID_Cooperado " +
                                                    "from Refracto_Info where Estado_Data= 1", conexion))
            {
                try
                {
                    conexion.Open();
                    using (var rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            _userSession.Id_RecepcionUva = rdr.GetInt32(0);
                            _userSession.NUM_TICKET = rdr.GetInt32(1);
                            _userSession.Nombre_Cooperado = rdr.GetString(2);
                            _userSession.Intento = rdr.GetInt32(3);
                            _userSession.Nombre_Planta = rdr.GetString(4);
                            _userSession.RUT_Cooperado = rdr.GetString(5);
                            _userSession.ID_Cooperado = rdr.GetInt32(6);
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
            using (var command = new SQLiteCommand("Update ESTADO_APP SET Estado = @Status;", conexion))
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
    
        public static void CambiarEstado_Data(int Valor, Session _userSession)
        {
            using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
            using (var command = new SQLiteCommand("Update Refracto_Info SET Estado_Data = @Status WHERE " +
                                            "ID_Recepcion_Uva = @Recepcion and NUM_TICKET=@Ticket", conexion))
            {
                try
                {
                    conexion.Open();
                    command.Parameters.Add("@Status", System.Data.DbType.Int32).Value = Valor;
                    command.Parameters.Add("@Recepcion", System.Data.DbType.Int32).Value = _userSession.Id_RecepcionUva;
                    command.Parameters.Add("@Ticket", System.Data.DbType.Int32).Value = _userSession.NUM_TICKET;
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
                catch (Exception)
                {
                    EscribirEnLog("Error al actualizar el estado de la recepción.");
                }
            }
        }
    
        public static void EliminarRegistro(int _idRecepcion)
        {
            try
            {
                using (var conexion = new SQLiteConnection("Data Source=" + FullPathDB + ";Version=3"))
                using (var command = new SQLiteCommand("DELETE FROM REFRACTO_INFO WHERE ID_RECEPCION_UVA = " + _idRecepcion + "", conexion))
                {
                    conexion.Open();
                    command.ExecuteNonQuery();
                    conexion.Close();
                }
            }
            catch (SQLiteException error)
            {
                EscribirEnLog("Error al eliminar el registro con Recepcion : " + _idRecepcion + "." +
                    "Error :" + error.Message);
            }
        }
    
    }
}
