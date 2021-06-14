using Producto.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Producto.Persistence.Interfaces
{
    public interface IProductoRepository
    {
        int ProductoInsertar(SqlConnection con, EProducto oEProducto);
        int ProductoActualizar(SqlConnection con, EProducto oEProducto);
        EProducto ProductoGetId(SqlConnection con, EProducto oEProducto);
        List<EProductoDetalle> ProductoDetalleListar(SqlConnection con, EProducto oEProducto);
    }
}
