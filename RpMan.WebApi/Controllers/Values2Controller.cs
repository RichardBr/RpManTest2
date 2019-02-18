using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RpMan.WebApi.Controllers
{

    //[Route("api/[controller]")]
    [ApiVersion("3.0")]
    [ApiVersion("4.0")]
    [Route("api/v{version:apiVersion}/values")]
    // [ApiController]
    [AllowAnonymous]
    public class Values2Controller : RpManControllerBase
    {
        // GET api/values
        [HttpGet, MapToApiVersion("3.0")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value3-1", "value3-2" };
        }

        [HttpGet, MapToApiVersion("4.0")]
        //[HttpGet("GetV4")]
        public ActionResult<IEnumerable<string>> GetV4()
        {
            return new string[] { "value4-1", "value4-2" };
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value"+id.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
