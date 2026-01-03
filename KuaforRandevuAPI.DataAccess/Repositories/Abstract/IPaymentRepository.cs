using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Abstract
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetPaymentsForToday();
        Task<List<Payment>> GetPaymentsForThisWeek();
        Task<List<Payment>> GetPaymentsForThisMonth();
        Task<List<Payment>> GetPaymentsWithDate(DateTime startDate, DateTime endDate);
        Task<List<Payment>> GetPaymentsByBarberIdWithDate(int barberId, DateTime startDate, DateTime endDate);
        Task<List<Payment>> GetOnCreditPaymentsWithDate(DateTime startDate, DateTime endDate);
        Task<List<Payment>> GetCashPaymentsWithDate(DateTime startDate, DateTime endDate);
        Task<List<Payment>> GetCreditCardPaymentsWithDate(DateTime startDate, DateTime endDate);
        Task<Payment?> GetLastPayment();
    }
}