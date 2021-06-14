using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Producto.Service.BusinessLogic
{
    public class Conexion
    {
        public string CadenaConexion { get; set; }
        public Conexion()
        {
            try
            {

                CadenaConexion = "Data Source=.; Initial Catalog= DBProducto; User ID= sa; Password=gertru";
            }
            catch(Exception ex)
            {
                CadenaConexion = "Data Source=.; Initial Catalog= DBProducto; User ID= sa; Password=gertru";
            }
            
        }

    }
}
