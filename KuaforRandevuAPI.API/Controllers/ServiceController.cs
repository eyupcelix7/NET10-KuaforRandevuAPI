using AutoMapper;
using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Dtos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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
            var result = await _service.GetAllService();
            return Ok(result);
        }
        [HttpGet("GetServiceById")]
        public async Task<IActionResult> GetServiceById(int id)
        {
            var result = await _service.GetServiceById(id);
            return Ok(result);
        }
        [HttpGet("GetServicesByBarberId/{id}")]
        public async Task<IActionResult> GetServicesByBarberId(int id)
        {
            var result = await _service.GetServicesByBarberId(id);
            return Ok(result);
        }
        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService(CreateServiceDto dto)
        {
            var result = await _service.CreateService(_mapper.Map<CreateServiceDto>(dto));
            return Ok(result);
        }
        [HttpPut("UpdateService")]
        public async Task<IActionResult> UpdateService(UpdateServiceDto dto)
        {
            var result = await _service.UpdateService(dto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveService(int id)
        {
            var result = await _service.RemoveService(id);
            return Ok(result);
        }
    }
}
