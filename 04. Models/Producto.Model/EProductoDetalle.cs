using System;
using System.Collections.Generic;
using System.Text;

namespace Producto.Model
{
    public class EProductoDetalle
    {
        public int idProductoDetalle { get; set; }
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public string fechaVencimiento { get; set; }

    }
}
