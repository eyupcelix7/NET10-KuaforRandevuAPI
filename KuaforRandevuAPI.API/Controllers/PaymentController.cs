using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Dtos.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KuaforRandevuAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service)   
        {
            _service = service;
        }
        [HttpGet("GetAllPayments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var result = await _service.GetAllPayments();
            return Ok(result);
        }
        [HttpGet("GetPaymentById/{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var result = await _service.GetPaymentById(id);
            return Ok(result);
        }
        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(CreatePaymentDto dto)
        {
            var result = await _service.CreatePayment(dto);
            return Ok(result);
        }
        [HttpPut("UpdatePayment")]
        public async Task<IActionResult> UpdatePayment(UpdatePaymentDto dto)
        {
            var result = await _service.UpdatePayment(dto);
            return Ok(result);
        }
        [HttpDelete("RemovePayment/{id}")]
        public async Task<IActionResult> RemovePayment(int id)
        {
            var result = _service.RemovePayment(id);
            return Ok(result);
        }
    }
}
