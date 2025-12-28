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
        [HttpGet("GetAllBarbers")]
        public async Task<IActionResult> GetAllBarbers()
        {
            var barbers = await _service.GetAllBarber();
            return Ok(barbers);
        }
        [HttpGet("GetBarberById/{id}")]
        public async Task<IActionResult> GetBarberById(int id)
        {
            var barber = await _service.GetBarberById(id);
            if(barber == null)
            {
                return NotFound();
            }
            return Ok(barber);
        }
        [HttpPost("CreateBarber")]
        public async Task<IActionResult> CreateBarber(CreateBarberDto dto)
        {
            try
            {
                await _service.CreateBarber(dto);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    error = ex.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                });
            }
        }
        [HttpGet("GetBarberByIdWithServices/{id}")]
        public async Task<IActionResult> GetBarberByIdWithServices(int id)
        {
            var values = await _service.GetBarberByIdWithServices(id);
            return Ok(values);
        }
        [HttpPut("UpdateBarber")]
        public async Task<IActionResult> UpdateBarber(UpdateBarberDto dto)
        {
            try
            {
                await _service.UpdateBarber(dto);
                return Ok(dto);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new
                {
                    message = ex.Errors.Select(x => x.ErrorMessage).FirstOrDefault()
                });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveBarber(int id)
        {
            await _service.RemoveBarber(id);
            return Ok("Silme işlemi başarılı");
        }
    }
}
