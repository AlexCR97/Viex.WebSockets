using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.WebSockets.Core.Models;

namespace Viex.WebSockets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameConceptController : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<ActionResult<IList<string>>> GetCategories()
        {
            var concepts = GameConceptCategories.All;
            return Ok(concepts);
        }
    }
}
