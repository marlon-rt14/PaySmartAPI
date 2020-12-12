using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConectarDatos;

namespace PaySmartAPI.Controllers
{
    public class ProductosController : ApiController
    {

        private ProductoEntities PaySmartContext = new ProductoEntities();

        //Visualiza todos los registros (api/producto)
        [HttpGet]
        public IEnumerable<producto> GetProducto()
        {
            try
            {
                using (ProductoEntities EntidadProducto = new ProductoEntities())
                {
                    return EntidadProducto.productos.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
            //return PaySmartContext.productos.ToList();
        }

        //Visualiza un registro (api/producto by ID)
        [HttpGet]
        public producto GetProductoByID(int id)
        {
            using (ProductoEntities EntidadProducto = new ProductoEntities())
            {
                return EntidadProducto.productos.FirstOrDefault(e => e.ID == id);
            }
            //return PaySmartContext.productos.FirstOrDefault(e => e.ID == id);
        }

        [HttpPost]
        public IHttpActionResult SetProducto([FromBody] producto NewProducto)
        {
            if (ModelState.IsValid)
            {
                PaySmartContext.productos.Add(NewProducto);
                PaySmartContext.SaveChanges();
                return Ok(NewProducto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateProducto(int id, [FromBody] producto CurrentProducto)
        {
            if(ModelState.IsValid)
            {
                var ProductoExistente = PaySmartContext.productos.Count(c => c.ID == id) > 0;
                if(ProductoExistente)
                {
                    PaySmartContext.Entry(CurrentProducto).State = EntityState.Modified;
                    PaySmartContext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteProducto(int id)
        {
            var ThisProducto = PaySmartContext.productos.Find(id);
            if(ThisProducto != null)
            {
                PaySmartContext.productos.Remove(ThisProducto);
                PaySmartContext.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
