using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductBL _bl;
        public ProductController(IProductBL bl)
        {
            _bl = bl;
        }
        [HttpGet]
        public ActionResult Get()
        {
           var result= _bl.GetAll();
            return CreateResponse(result);
        }
        private ActionResult CreateResponse<T>(IBL.Data.IResponse<T> result)
        {
           switch(result.Status)
            {
                case IBL.Data.ActionStatus.Success:
                    return StatusCode(200, result.Data);
                case IBL.Data.ActionStatus.ValidationError:
                    return StatusCode(400, result.Message);
                default:
                    return StatusCode(500, result.Message);
            }
        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(Guid id)
        {
            return CreateResponse(_bl.Get(id));
        }


        [HttpPost]
        public ActionResult Post([FromBody]  Product product)//create
        {

            return CreateResponse(_bl.Add(product));
        }

        public ActionResult Put( [FromBody] Product product)//update
        {
            return CreateResponse(_bl.Update(product));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            return CreateResponse(_bl.Delete(id));
        }
    }
}
