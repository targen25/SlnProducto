using Producto.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Producto.Service.Interfaces
{
    public interface IProductoService
    {
        
        int ProductoInsertar(EProducto oEProducto);
        int ProductoActualizar(EProducto oEProducto);
        EProducto ProductoGetId(EProducto oEProducto);
        List<EProductoDetalle> ProductoDetalleGetId(EProducto oEProducto);

    }
}
