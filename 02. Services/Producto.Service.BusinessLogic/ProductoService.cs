using Producto.Model;
using Producto.Persistence.Data;
using Producto.Persistence.Interfaces;
using Producto.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Producto.Service.BusinessLogic
{
    public class ProductoService: IProductoService
    {

        public int ProductoInsertar(EProducto oEProducto)
        {
            Conexion oConexion = new Conexion();
            IProductoRepository _IProductoRepository = new ProductoRepository();
            int id = 0;

            using (SqlConnection con = new SqlConnection(oConexion.CadenaConexion))
            {
                try
                {
                    con.Open();
                    id = _IProductoRepository.ProductoInsertar(con, oEProducto);
                }
                catch (Exception ex)
                {
                    id = -1;
                }
            }
            return id;
        }
        public int ProductoActualizar(EProducto oEProducto)
        {
            Conexion oConexion = new Conexion();
            IProductoRepository _IProductoRepository = new ProductoRepository();
            int id = 0;
            using (SqlConnection con = new SqlConnection(oConexion.CadenaConexion))
            {
                try
                {
                    con.Open();
                    id = _IProductoRepository.ProductoActualizar(con, oEProducto);
                }
                catch (Exception ex)
                {
                    id = -1;
                }
            }
            return id;
        }

        public EProducto ProductoGetId(EProducto oEProducto)
        {
            Conexion oConexion = new Conexion();
            IProductoRepository _IProductoRepository = new ProductoRepository();
            int id = 0;
            EProducto oEProductoResul = new EProducto();
            using (SqlConnection con = new SqlConnection(oConexion.CadenaConexion))
            {
                try
                {
                    con.Open();
                    oEProductoResul = _IProductoRepository.ProductoGetId(con, oEProducto);
                }
                catch (Exception ex)
                {
                    oEProductoResul = null;
                }
            }
            return oEProductoResul;

        }
        public List<EProductoDetalle> ProductoDetalleGetId(EProducto oEProducto)
        {
            Conexion oConexion = new Conexion();
            IProductoRepository _IProductoRepository = new ProductoRepository();
            int id = 0;
            List<EProductoDetalle> oEProductoListResul = new List<EProductoDetalle>();
            using (SqlConnection con = new SqlConnection(oConexion.CadenaConexion))
            {
                try
                {
                    con.Open();
                    oEProductoListResul = _IProductoRepository.ProductoDetalleListar(con, oEProducto);
                }
                catch (Exception ex)
                {
                    oEProductoListResul = null;
                }
            }
            return oEProductoListResul;

        }

    }
}
