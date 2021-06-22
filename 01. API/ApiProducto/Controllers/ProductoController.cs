using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Producto.Common;
using Producto.Model;
using Producto.Service.BusinessLogic;
using Producto.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiProducto.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IMemoryCache _memoryCache;
        public ProductoController(ILogger<ProductoController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }
 
        [HttpPost]
        [Route("ProductoInsertar")]
        public IActionResult ProductoInsertar(EProducto oEProducto)
        {
            int id = 0;
            string mesajeResp = "";
            IProductoService oProductoService = new ProductoService();

            id = oProductoService.ProductoInsertar(oEProducto);
            if (id > 0)
            {
                mesajeResp = "El producto se registro satisfactoriamente";
                //grabar datos en cache
                //pais
                if (!_memoryCache.TryGetValue("keyPais",out string cachePais))
                {
                    cachePais = oEProducto.pais;
                    var cacheOption = new MemoryCacheEntryOptions()
                    {
                        Priority = CacheItemPriority.High,
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        Size = 128
                    };
                    _memoryCache.Set("keyPais", cachePais, cacheOption);
                }
                //ciudad
                if (!_memoryCache.TryGetValue("keyCiudad", out string cacheCiudad))
                {
                    cacheCiudad = oEProducto.ciudad;
                    var cacheOption = new MemoryCacheEntryOptions()
                    {
                        Priority = CacheItemPriority.High,
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        Size = 128
                    };
                    _memoryCache.Set("keyCiudad", cacheCiudad, cacheOption);
                }

            }
            else
            {
                mesajeResp = "No se registro el producto";
            }
            return Ok(mesajeResp);

        }
        [HttpPut]
        [Route("ProductoActualizar")]
        public IActionResult ProductoActualizar(EProducto oEProducto)
        {
            int id = 0;
            string mesajeResp = "";
            IProductoService oProductoService = new ProductoService();

            id = oProductoService.ProductoActualizar(oEProducto);
            if(id > 0)
            {
                mesajeResp = "El producto se actualizo satisfactoriamente";
                //grabar datos en cache
                //pais
                if (!_memoryCache.TryGetValue("keyPais", out string cachePais))
                {
                    cachePais = oEProducto.pais;
                    var cacheOption = new MemoryCacheEntryOptions()
                    {
                        Priority = CacheItemPriority.High,
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        Size = 128
                    };
                    _memoryCache.Set("keyPais", cachePais, cacheOption);
                }
                //ciudad
                if (!_memoryCache.TryGetValue("keyCiudad", out string cacheCiudad))
                {
                    cacheCiudad = oEProducto.ciudad;
                    var cacheOption = new MemoryCacheEntryOptions()
                    {
                        Priority = CacheItemPriority.High,
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        Size = 128
                    };
                    _memoryCache.Set("keyCiudad", cacheCiudad, cacheOption);
                }

            }
            else
            {
                mesajeResp = "No se actualizo el producto";
            }

            return Ok(mesajeResp);
        }

        [HttpGet]
        [Route("ProductoGetId")]
        public IActionResult ProductoGetId(EProducto oEProducto)
        {
            string comentario = "";
            EProducto oEProductoResul = new EProducto();
            IProductoService oProductoService = new ProductoService();
            //---------------recuperar data de la base de datos
            oEProductoResul = oProductoService.ProductoGetId(oEProducto);
            oEProductoResul.ListaDetalle= oProductoService.ProductoDetalleGetId(oEProducto);
            //---------------recuperar datos del cache
            try
            {
                oEProductoResul.cachePais = _memoryCache.Get("keyPais").ToString();
                oEProductoResul.cacheCiudad = _memoryCache.Get("keyCiudad").ToString();

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error :"+ex.Message);
                Logger.RegistroLog(ex.Message);
                oEProductoResul.cachePais = "El cache expiró";
                oEProductoResul.cacheCiudad = "El cache expiró";

            }
            //-------------------recuperar data de servicio externo
            string idComentario = "1";//ese parametro puede venir de base de datos
            ServicioExterno oServicioExterno = new ServicioExterno();
            string jsonComentarios = oServicioExterno.enviarJsonGet();
            var listaEmpresasInterOperables = JsonConvert.DeserializeObject<List<EComentario>>(jsonComentarios);
            if (listaEmpresasInterOperables != null)
            {
                var resultadoFiltroComentario = listaEmpresasInterOperables.Find(x=> x.id== idComentario);
                comentario = resultadoFiltroComentario.name;
            }
            else
            {
                comentario = "No hay dato en el servicio externo";
            }
            oEProductoResul.datoServicioExterno = comentario;


            return Ok(oEProductoResul);
        }
    }
}
