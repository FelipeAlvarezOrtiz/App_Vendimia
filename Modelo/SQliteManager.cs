using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Romana_AppVendimia.Modelo
{
    static class SQliteManager
    {
        private readonly static string CreacionDB = "";

        public static string NombreDB = "Romana.db";
        public static string FullPathDB = @"C:/ROMANA/DB/"+NombreDB;

    }
}
