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
        public PaymentService(IRepository<Payment> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
    }
}
