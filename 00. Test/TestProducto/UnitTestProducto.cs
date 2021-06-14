using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Producto.Model;
using Producto.Service.BusinessLogic;
using Producto.Service.Interfaces;

namespace TestProducto
{
    [TestClass]
    public class UnitTestProducto
    {
        
        [TestMethod]
        public void ProductoInsertar_test()
        {
            //parametros
            EProducto oEProducto = new EProducto();
            oEProducto.descripcion = "Producto de test";
            oEProducto.pais = "Brazil";
            oEProducto.ciudad = "Rio de Janeiro";

            List<EProductoDetalle> listaDetalle = new List<EProductoDetalle>();
            EProductoDetalle oEProductoDetalle = new EProductoDetalle();
            oEProductoDetalle.nombreProducto = "detalle test";
            oEProductoDetalle.cantidad = 10;
            oEProductoDetalle.precio = Convert.ToDecimal(12.5);
            oEProductoDetalle.fechaVencimiento ="12/06/2022";
            listaDetalle.Add(oEProductoDetalle);

            oEProducto.ListaDetalle = listaDetalle;

            int valorEsperado = 1;
            //invocar al metodo a testear
            
           // int valorActual= oProductoService.ProductoInsertar(oEProducto);
            var mock = new Mock<IProductoService>();
            mock.Setup(p => p.ProductoInsertar(oEProducto)).Returns(1);
            IProductoService oProductoService = new ProductoService();
            int valorActual = oProductoService.ProductoInsertar(oEProducto);
           
            //comprobar valor esperado sea igual al valor actual
            Assert.AreEqual(valorEsperado, valorActual);
        }
        
        [TestMethod]
        public void ProductoActualizar_test()
        {
            //parametros
            EProducto oEProducto = new EProducto();
            oEProducto.idProducto = 6;
            oEProducto.descripcion = "Útiles";
            oEProducto.pais = "Perú";
            oEProducto.ciudad = "Cusco";

            List<EProductoDetalle> listaDetalle = new List<EProductoDetalle>();
            EProductoDetalle oEProductoDetalle = new EProductoDetalle();
            oEProductoDetalle.idProducto = 6;
            oEProductoDetalle.idProductoDetalle = 20;
            oEProductoDetalle.nombreProducto = "Cuaderno 1";
            oEProductoDetalle.cantidad = 33;
            oEProductoDetalle.precio = Convert.ToDecimal(12.26);
            oEProductoDetalle.fechaVencimiento = "17/12/2023";

            listaDetalle.Add(oEProductoDetalle);

            oEProducto.ListaDetalle = listaDetalle;

            int valorEsperado = 6;
            //invocar al metodo a testear
            IProductoService oProductoService = new ProductoService();
            int valorActual = oProductoService.ProductoActualizar(oEProducto);
            //comprobar valor esperado sea igual al valor actual
            Assert.AreEqual(valorEsperado, valorActual);

        }
        
        
          [TestMethod]
          public void ProductoGetId_test()
          {
              //parametros
              EProducto oEProducto = new EProducto();
              oEProducto.idProducto = 6;
              oEProducto.descripcion = "Útiles";
              oEProducto.pais = "Perú";
              oEProducto.ciudad = "Cusco";
              oEProducto.fechaIngreso = "13/06/2021";

              int idProductoEsperado = oEProducto.idProducto;
              string descripcionEsperado = oEProducto.descripcion;
              //invocar al metodo a testear
              IProductoService oProductoService = new ProductoService();
              EProducto oEProductoActual= oProductoService.ProductoGetId(oEProducto);
              int idProductoActual = oEProductoActual.idProducto;
              string descripcionActual = oEProductoActual.descripcion;
              //comprobar valor esperado sea igual al valor actual
              Assert.IsInstanceOfType(oEProductoActual, typeof(EProducto));
              Assert.AreEqual(idProductoEsperado, idProductoActual);
              Assert.AreEqual(descripcionEsperado, descripcionActual);
          }
         
        [TestMethod]
        public void ProductoDetalleGetId_test()
        {
            //parametros
            EProducto oEProducto = new EProducto();
            oEProducto.idProducto = 6;
            //detalle
            EProductoDetalle oEProductoDetalle = new EProductoDetalle();
            oEProductoDetalle.idProductoDetalle = 20;
            oEProductoDetalle.idProducto = 6;
            oEProductoDetalle.nombreProducto = "Cuaderno 1";
            oEProductoDetalle.cantidad = 33;
            oEProductoDetalle.precio = Convert.ToDecimal(12.26);
            oEProductoDetalle.fechaVencimiento = "17/12/2023";

            int idProductoEsperado = oEProductoDetalle.idProductoDetalle;
            int idProductoDetalleEsperado = oEProductoDetalle.idProducto;
            string nombreProductoEsperado= oEProductoDetalle.nombreProducto;
            //invocar al metodo a testear
            IProductoService oProductoService = new ProductoService();
            List<EProductoDetalle> oEProductoDetalleActual = oProductoService.ProductoDetalleGetId(oEProducto);
            int idProductoActual = oEProductoDetalleActual[0].idProductoDetalle;
            int idProductoDetalleActual = oEProductoDetalleActual[0].idProducto;
            string nombreProductoActual = oEProductoDetalleActual[0].nombreProducto;

            //comprobar valor esperado sea igual al valor actual
            Assert.IsInstanceOfType(oEProductoDetalleActual, typeof(List<EProductoDetalle>));
            Assert.AreEqual(idProductoEsperado, idProductoActual);
            Assert.AreEqual(idProductoDetalleEsperado, idProductoDetalleActual);
            Assert.AreEqual(nombreProductoEsperado, nombreProductoActual);
        }
        
    }
}
