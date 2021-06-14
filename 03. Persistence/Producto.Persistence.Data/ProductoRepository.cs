using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Producto.Model;
using Producto.Persistence.Interfaces;

namespace Producto.Persistence.Data
{
    public class ProductoRepository: IProductoRepository
    {
        public int ProductoInsertar(SqlConnection con, EProducto oEProducto)
        {
            int idProducto = 0;
            int idDetalle = 0;
            SqlTransaction transaction;
            SqlCommand cmd = new SqlCommand("uspProductoInsertar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            transaction = con.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                cmd.Parameters.Add(new SqlParameter("@descripcion", oEProducto.descripcion));
                cmd.Parameters.Add(new SqlParameter("@pais", oEProducto.pais));
                cmd.Parameters.Add(new SqlParameter("@ciudad", oEProducto.ciudad));

                SqlParameter parOut = cmd.Parameters.Add("@@identity", SqlDbType.Int);
                parOut.Direction = ParameterDirection.ReturnValue;

                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    idProducto = (int)parOut.Value;
                    //insert detalle
                    if (oEProducto.ListaDetalle != null)
                    {
                        foreach (var detalle in oEProducto.ListaDetalle)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "uspProductoDetalleInsertar";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@idProducto", idProducto));
                            cmd.Parameters.Add(new SqlParameter("@nombreProducto", detalle.nombreProducto));
                            cmd.Parameters.Add(new SqlParameter("@cantidad", detalle.cantidad));
                            cmd.Parameters.Add(new SqlParameter("@precio", detalle.precio));
                            cmd.Parameters.Add(new SqlParameter("@fechaVencimiento", Convert.ToDateTime(detalle.fechaVencimiento)));
                            idDetalle = cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();

                }
                
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                idProducto = 0;
            }
            if (idProducto > 0)
            {
                idProducto = 1;
            }
            return idProducto;
        }

        public int ProductoActualizar(SqlConnection con, EProducto oEProducto)
        {
            int idProducto = 0;
            int idDetalle = 0;
            SqlTransaction transaction;
            SqlCommand cmd = new SqlCommand("uspProductoActualizar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            transaction = con.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                cmd.Parameters.Add(new SqlParameter("@idProducto", oEProducto.idProducto));
                cmd.Parameters.Add(new SqlParameter("@descripcion", oEProducto.descripcion));
                cmd.Parameters.Add(new SqlParameter("@pais", oEProducto.pais));
                cmd.Parameters.Add(new SqlParameter("@ciudad", oEProducto.ciudad));

                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    idProducto = oEProducto.idProducto;
                    //actualizar detalle
                    if (oEProducto.ListaDetalle != null)
                    {
                        foreach (var detalle in oEProducto.ListaDetalle)
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "uspProductoDetalleActualizar";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@idProductoDetalle", detalle.idProductoDetalle));
                            cmd.Parameters.Add(new SqlParameter("@idProducto", oEProducto.idProducto));
                            cmd.Parameters.Add(new SqlParameter("@nombreProducto", detalle.nombreProducto));
                            cmd.Parameters.Add(new SqlParameter("@cantidad", detalle.cantidad));
                            cmd.Parameters.Add(new SqlParameter("@precio", detalle.precio));
                            cmd.Parameters.Add(new SqlParameter("@fechaVencimiento", Convert.ToDateTime(detalle.fechaVencimiento)));
                            idDetalle = cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                idProducto = 0;

            }
            return idProducto;
        }
        public EProducto ProductoGetId(SqlConnection con, EProducto oEProducto)
        {
            EProducto producto = null;
            try
            {
                SqlCommand cmd = new SqlCommand("uspProductoGetId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idProducto", oEProducto.idProducto));
                SqlDataReader drd = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                while (drd.Read())
                {
                    producto = new EProducto();
                    producto.idProducto = Convert.ToInt32(drd["idProducto"]);
                    producto.descripcion = drd["descripcion"].ToString();
                    producto.pais = drd["pais"].ToString();
                    producto.ciudad = drd["ciudad"].ToString();
                    producto.fechaIngreso = Convert.ToDateTime(drd["fechaIngreso"].ToString()).ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                producto = null;
            }
                return producto;            
        }
        public List<EProductoDetalle> ProductoDetalleListar(SqlConnection con, EProducto oEProducto)
        {
            List<EProductoDetalle> listProductoDet = new List<EProductoDetalle>();
            EProductoDetalle productoDet = null;
            try
            {
                SqlCommand cmd = new SqlCommand("uspProductoDetalleGetId", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@idProducto", oEProducto.idProducto));
                SqlDataReader drd = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                while (drd.Read())
                {
                    productoDet = new EProductoDetalle();
                    productoDet.idProductoDetalle = Convert.ToInt32(drd["idProductoDetalle"]);
                    productoDet.idProducto = Convert.ToInt32(drd["idProducto"]);
                    productoDet.nombreProducto = drd["nombreProducto"].ToString();
                    productoDet.cantidad = Convert.ToInt32(drd["cantidad"]);
                    productoDet.precio = Convert.ToDecimal(drd["precio"]);
                    productoDet.fechaVencimiento = Convert.ToDateTime(drd["fechaVencimiento"].ToString()).ToString("dd/MM/yyyy");
                    listProductoDet.Add(productoDet);
                }

            }
            catch (Exception ex)
            {
                listProductoDet = null;
            }


            return listProductoDet;
        }



    }
}
