using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebserviceApp.Data;

namespace MyApp.Namespace
{
    [Route("api/webservice")]
    [ApiController]
    public class WebserviceApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context; 

        public WebserviceApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/webservice 
        [HttpGet]
        public async Task<IActionResult> GetWebservices() {
            //check if null

            if(_context.Webservices == null){
                return NotFound();
            }

            return Ok(await _context.Webservices.ToListAsync());

        }

    }
}
