using AutoMapper;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Common.Responses;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Payment;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _repository;
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IRepository<Payment> repository, IMapper mapper, IPaymentRepository paymentRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _paymentRepository = paymentRepository;
        }
        public async Task<ApiResponse<List<ResultPaymentDto>>> GetAllPayments()
        {
            var payments = await _repository.GetAll();
            var data = _mapper.Map<List<ResultPaymentDto>>(payments);
            return ApiResponse<List<ResultPaymentDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<ResultPaymentDto>> GetPaymentById(int id)
        {
            var payment = await _repository.GetById(id);
            var data = _mapper.Map<ResultPaymentDto>(payment);
            return ApiResponse<ResultPaymentDto>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<CreatePaymentDto>> CreatePayment(CreatePaymentDto dto)
        {
            await _repository.Add(_mapper.Map<Payment>(dto));
            return ApiResponse<CreatePaymentDto>.SuccessResponse(dto, "OK");
        }
        public async Task<ApiResponse<UpdatePaymentDto>> UpdatePayment(UpdatePaymentDto dto)
        {
            var updatedPayment = await _repository.GetById(dto.Id);
            updatedPayment.BarberId = dto.BarberId;
            updatedPayment.ReservationId = dto.ReservationId;
            updatedPayment.Date = dto.Date;
            updatedPayment.Amount = dto.Amount;
            updatedPayment.PaymentMethod = dto.PaymentMethod;
            await _repository.Update(updatedPayment);
            return ApiResponse<UpdatePaymentDto>.SuccessResponse(dto, "OK");
        }
        public async Task<ApiResponse<int>> RemovePayment(int id)
        {
            var payment = await _repository.GetById(id);
            await _repository.Remove(payment);
            return ApiResponse<int>.SuccessResponse(id, "OK");
        }
        public async Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsForToday()
        {
            var payments = await _paymentRepository.GetPaymentsForToday();
            var data = _mapper.Map<List<ResultPaymentDto>>(payments);
            return ApiResponse<List<ResultPaymentDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsForThisWeek()
        {
            var payments = await _paymentRepository.GetPaymentsForThisWeek();
            var data = _mapper.Map<List<ResultPaymentDto>>(payments);
            return ApiResponse<List<ResultPaymentDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsForThisMonth()
        {
            var payments = await _paymentRepository.GetPaymentsForThisMonth();
            var data = _mapper.Map<List<ResultPaymentDto>>(payments);
            return ApiResponse<List<ResultPaymentDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsWithDate(DateTime startDate, DateTime endDate)
        {
            var payments = await _paymentRepository.GetPaymentsWithDate(startDate, endDate);
            var data = _mapper.Map<List<ResultPaymentDto>>(payments);
            return ApiResponse<List<ResultPaymentDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsByBarberIdWithDate(int barberId, DateTime startDate, DateTime endDate)
        {
            var payments = await _paymentRepository.GetPaymentsByBarberIdWithDate(barberId, startDate, endDate);
            var data = _mapper.Map<List<ResultPaymentDto>>(payments);
            return ApiResponse<List<ResultPaymentDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsByOnCreditWithDate(DateTime startDate, DateTime endDate)
        {
            var payments = await _paymentRepository.GetOnCreditPaymentsWithDate(startDate, endDate);
            var data = _mapper.Map<List<ResultPaymentDto>>(payments);
            return ApiResponse<List<ResultPaymentDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsByCashWithDate(DateTime startDate, DateTime endDate)
        {
            var payments = await _paymentRepository.GetCashPaymentsWithDate(startDate, endDate);
            var data = _mapper.Map<List<ResultPaymentDto>>(payments);
            return ApiResponse<List<ResultPaymentDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsByCreditCardWithDate(DateTime startDate, DateTime endDate)
        {
            var payments = await _paymentRepository.GetCreditCardPaymentsWithDate(startDate, endDate);
            var data = _mapper.Map<List<ResultPaymentDto>>(payments);
            return ApiResponse<List<ResultPaymentDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<ResultLastPaymentDto?>> GetLastPaymentWithCustomer()
        {
            var payments = await _paymentRepository.GetLastPaymentWithCustomer();
            var data = _mapper.Map<ResultLastPaymentDto>(payments);
            return ApiResponse<ResultLastPaymentDto?>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<UpdatePaymentMethodDto>> UpdatePaymentMethod(UpdatePaymentMethodDto dto)
        {
            var payment = await _repository.GetById(dto.Id);
            payment.PaymentMethod = dto.PaymentMethod;
            await _paymentRepository.UpdatePaymentMethod(payment);
            return ApiResponse<UpdatePaymentMethodDto>.SuccessResponse(dto, "OK");
        }
    }
}
