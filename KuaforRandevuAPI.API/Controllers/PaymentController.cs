using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Dtos.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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
        [HttpGet("GetPaymentsForToday")]
        public async Task<IActionResult> GetPaymentsForToday()
        {
            var result = await _service.GetPaymentsForToday();
            return Ok(result);
        }
        [HttpGet("GetPaymentsForThisWeek")]
        public async Task<IActionResult> GetPaymentsForThisWeek()
        {
            var result = await _service.GetPaymentsForThisWeek();
            return Ok(result);
        }
        [HttpGet("GetPaymentsForThisMonth")]
        public async Task<IActionResult> GetPaymentsForThisMonth()
        {
            var result = await _service.GetPaymentsForThisMonth();
            return Ok(result);
        }
        [HttpGet("GetPaymentsWithDate")]
        public async Task<IActionResult> GetPaymentsWithDate(DateTime startDate, DateTime endDate)
        {
            var result = await _service.GetPaymentsWithDate(startDate, endDate);
            return Ok(result);
        }
        [HttpGet("GetPaymentsByBarberIdWithDate")]
        public async Task<IActionResult> GetPaymentsByBarberIdWithDate(int barberId, DateTime startDate, DateTime endDate)
        {
            var result = await _service.GetPaymentsByBarberIdWithDate(barberId, startDate, endDate);
            return Ok(result);
        }
        [HttpGet("GetPaymentsByOnCreditWithDate")]
        public async Task<IActionResult> GetPaymentsByOnCreditWithDate(DateTime startDate, DateTime endDate)
        {
            var result = await _service.GetPaymentsByOnCreditWithDate(startDate, endDate);
            return Ok(result);
        }
        [HttpGet("GetPaymentsByCashWithDate")]
        public async Task<IActionResult> GetPaymentsByCashWithDate(DateTime startDate, DateTime endDate)
        {
            var result = await _service.GetPaymentsByCashWithDate(startDate, endDate);
            return Ok(result);
        }
        [HttpGet("GetPaymentsByCreditCardWithDate")]
        public async Task<IActionResult> GetPaymentsByCreditCardWithDate(DateTime startDate, DateTime endDate)
        {
            var result = await _service.GetPaymentsByCreditCardWithDate(startDate, endDate);
            return Ok(result);
        }
        [HttpGet("GetLastPaymentWithCustomer")]
        public async Task<IActionResult> GetLastPaymentWithCustomer()
        {
            var result = await _service.GetLastPaymentWithCustomer();
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
        [HttpPut("UpdatePaymentMethod")]
        public async Task<IActionResult> UpdatePaymentMethod(UpdatePaymentMethodDto dto)
        {
            var result = await _service.UpdatePaymentMethod(dto);
            //return Ok(result);
            return StatusCode(result.Status, result);
        }
        [HttpDelete("RemovePayment/{id}")]
        public async Task<IActionResult> RemovePayment(int id)
        {
            var result = await _service.RemovePayment(id);
            return Ok(result);
        }
    }
}
