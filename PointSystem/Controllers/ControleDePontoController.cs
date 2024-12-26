using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PointSystem.Data;
using PointSystem.Model.Enum;
using PointSystem.Services;

namespace PointSystem.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ControleDePontoController : ControllerBase
    {
        private ControleDePontoService _service;

        public ControleDePontoController(ApplicationDbContext _context)
        {
            this._service = new ControleDePontoService(_context);
        }

        [HttpGet]
        public IActionResult Get() => Ok(_service.Get(User.FindFirst("idUser")?.Value));

        [HttpPost]
        public IActionResult Post(bool entrada) => Ok(_service.Add(User.FindFirst("idUser")?.Value, entrada == true ? TypePonto.Entrata : TypePonto.Saida));

    }
}
