using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Producto.Common
{
   public class Logger
    {
        public static void RegistroLog(string strMensaje)
        {
            try
            {
                string strDirectory = @"C:\LogNetProducto";
                string strFecha = DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
                string strLogFile = "LogFile_" + strFecha + ".txt";

                if (!Directory.Exists(strDirectory))
                {
                    Directory.CreateDirectory(strDirectory);
                }
                strLogFile = Path.Combine(strDirectory, strLogFile);

                using (StreamWriter swRegistro = new StreamWriter(strLogFile, true))
                {
                    swRegistro.WriteLine(string.Format(strMensaje, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                    swRegistro.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

    }
}
