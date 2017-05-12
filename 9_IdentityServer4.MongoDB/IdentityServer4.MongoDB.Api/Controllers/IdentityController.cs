using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4.MongoDB.Api.Controllers
{
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        // GET api/Get
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

    }
}
