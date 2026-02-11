using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error");
            return BadRequest();
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetValidationErrorResponse(int id)
        {
            return BadRequest();
        }
        [HttpGet("Unauthorized")]
        public ActionResult GetUnauthorizedResponse(int id)
        {
            return Unauthorized();
        }
    }
}
