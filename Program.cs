﻿using System;
using System.Threading;
using System.Windows.Forms;

namespace Romana_AppVendimia
{
    static class Program
    {
        static readonly Mutex _mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");

        [STAThread]
        static void Main()
        {
            if (_mutex.WaitOne(TimeSpan.Zero,true)) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Trabajador_Vista());
                _mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("LA APLICACIÓN YA SE ENCUENTRA ABIERTA.","ALERTA",
                    MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
