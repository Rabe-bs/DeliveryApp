using CRUD.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet("BadRequest")]
        public void BadRequesst()
        {
            throw new AppBadRequestException("BadRequest");
        }

        [HttpGet("NotFound")]
        public void NotFounds()
        {
            throw new AppNotFoundException("NotFound");
        }
    }
}
