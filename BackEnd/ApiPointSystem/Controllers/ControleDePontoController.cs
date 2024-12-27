using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PointSystem.Data;
using PointSystem.Model.Enum; 
using PointSystem.Services;

namespace PointSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin")]

    public class ControleDePontoController : ControllerBase
    {
        private ControleDePontoService _service;

        public ControleDePontoController(ApplicationDbContext _context)
        {
            this._service = new ControleDePontoService(_context);
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.Get(User.FindFirst("idUser")?.Value));

        [HttpPost("RegistrarEntrada")]
        public IActionResult Entrata() => Ok(_service.Add(User.FindFirst("idUser")?.Value, TypePonto.Entrata));
        
        [HttpPost("RegistrarSaida")]
        public IActionResult Saida() => Ok(_service.Add(User.FindFirst("idUser")?.Value,  TypePonto.Saida));

    }
}
