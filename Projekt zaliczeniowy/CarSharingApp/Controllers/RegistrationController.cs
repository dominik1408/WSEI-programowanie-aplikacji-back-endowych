using CarSharingApp.Dto;
using CarSharingApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarSharingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _service;

        public RegistrationController(IRegistrationService service)
        {
            _service = service;
        }

        [HttpPost("user")]
        public ActionResult Register([FromBody] RegistrationDto dto)
        {
            _service.Registartion(dto);
            return Ok();
        }

        [HttpPost("admin")]
        public ActionResult RegisterAdmin([FromBody] RegistrationAdminDto dto)
        {
            _service.RegistrationAdmin(dto);
            return Ok();
        }
    }
}
