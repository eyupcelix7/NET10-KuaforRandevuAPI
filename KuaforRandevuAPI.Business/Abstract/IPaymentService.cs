using KuaforRandevuAPI.Common.Responses;
using KuaforRandevuAPI.Dtos.Payment;
using KuaforRandevuAPI.Entities.Concrete;
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
        
        Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsForToday();
        Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsForThisWeek();
        Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsForThisMonth();
        Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsWithDate(DateTime startDate, DateTime endDate);
        Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsByBarberIdWithDate(int barberId, DateTime startDate, DateTime endDate);
        Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsByOnCreditWithDate(DateTime startDate, DateTime endDate);
        Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsByCashWithDate(DateTime startDate, DateTime endDate);
        Task<ApiResponse<List<ResultPaymentDto>>> GetPaymentsByCreditCardWithDate(DateTime startDate, DateTime endDate);
        Task<ApiResponse<ResultLastPaymentDto?>> GetLastPaymentWithCustomer();
        Task<ApiResponse<UpdatePaymentMethodDto>> UpdatePaymentMethod(UpdatePaymentMethodDto dto);
    }
}
