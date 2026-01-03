using KuaforRandevuAPI.Common.Responses;
using KuaforRandevuAPI.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Abstract
{
    public interface IPaymentService
    {
        Task<ApiResponse<List<ResultPaymentDto>>> GetAllPayments();
        Task<ApiResponse<ResultPaymentDto>> GetPaymentById(int id);
        Task<ApiResponse<CreatePaymentDto>> CreatePayment(CreatePaymentDto dto);
        Task<ApiResponse<UpdatePaymentDto>> UpdatePayment(UpdatePaymentDto dto);
        Task<ApiResponse<int>> RemovePayment(int id);
    }
}
