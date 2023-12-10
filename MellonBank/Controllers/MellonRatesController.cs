using MellonBank.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MellonBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MellonRatesController : ControllerBase
    {
        private readonly MellonRatesService _service;

        public MellonRatesController(MellonRatesService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetRates()
        {
            var rates = _service.GetRates();
            return rates != null ? Ok(rates) : BadRequest();
        }
    }
}
