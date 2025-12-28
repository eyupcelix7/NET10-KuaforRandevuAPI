using AutoMapper;
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
        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService(CreateServiceDto dto)
        {
            await _service.CreateService(_mapper.Map<CreateServiceDto>(dto));
            return Ok(dto);
        }
        [HttpPut("UpdateService")]
        public async Task<IActionResult> UpdateService(UpdateServiceDto dto)
        {
            await _service.UpdateService(dto);
            return Ok(dto);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveService(int id)
        {
            await _service.RemoveService(id);
            return Ok("Silme işlemi başarılı");
        }
    }
}
