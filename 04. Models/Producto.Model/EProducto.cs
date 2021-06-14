using System;
using System.Collections.Generic;

namespace Producto.Model
{
    public class EProducto
    {
        public int idProducto { get; set; }
        public string descripcion { get; set; }
        public string pais { get; set; }
        public string ciudad { get; set; }
        public string fechaIngreso { get; set; }
        public string cachePais { get; set; }
        public string cacheCiudad { get; set; }

        public string datoServicioExterno { get; set; }

        public List<EProductoDetalle> ListaDetalle { get; set; }
    }
}
