using AutoMapper;
using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Dtos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KuaforRandevuAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServiceService _service;
        public ServiceController(IMapper mapper, IServiceService service)
        {
            _mapper = mapper;
            _service = service;
        }
        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllServices()
        {
            var services = await _service.GetAllService();
            return Ok(services);
        }
        [HttpGet("GetServiceById")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var service = await _service.GetServiceById(id);
            return Ok(service);
        }
        [HttpGet("GetServicesByBarberId/{id}")]
        public async Task<IActionResult> GetServicesByBarberId(int id)
        {
            var values = await _service.GetServicesByBarberId(id);
            return Ok(values);
        }
        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService(CreateServiceDto dto)
        {
            try
            {
                await _service.CreateService(_mapper.Map<CreateServiceDto>(dto));
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
        [HttpPut("UpdateService")]
        public async Task<IActionResult> UpdateService(UpdateServiceDto dto)
        {
            try
            {
                await _service.UpdateService(dto);
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
        public async Task<IActionResult> RemoveService(int id)
        {
            await _service.RemoveService(id);
            return Ok("Silme işlemi başarılı");
        }
    }
}
