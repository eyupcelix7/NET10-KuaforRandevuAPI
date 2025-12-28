using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KuaforRandevuAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _service;
        public BarberController(IBarberService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBarber(CreateBarberDto dto)
        {
            try
            {
                _service.CreateBarber(dto);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }
    }
}
