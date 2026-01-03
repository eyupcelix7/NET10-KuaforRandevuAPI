using KuaforRandevuAPI.DataAccess.Context;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Entities.Concrete;
using KuaforRandevuAPI.Entities.Enums.PaymentMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BarberContext _context;
        public PaymentRepository(BarberContext context)
        {
            _context = context;
        }
        public async Task<List<Payment>> GetPaymentsForToday()
        {
            return await _context.Payments
                .Where(x => x.Date == DateTime.Today)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<Payment>> GetPaymentsForThisWeek()
        {
            // Pazartesiyi bulmak için bugun haftanın hangi günü +1 olarak alıyoruz ve geriye doğru saydırıyoruz.
            var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - +1);
            var sunday = monday.AddDays(6); // Pazar günü, mantıksal filtreleme de kullanılacak.
            return await _context.Payments
                .Where(x => x.Date >= monday && x.Date <= sunday)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<Payment>> GetPaymentsForThisMonth()
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 01); // Ayın başlangıcı
            DateTime lastDatOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1); // Ayın son günü için 1 ay ekleyip 1 gün çıkarıyoruz ve böylelikle son günü alıyoruz.
            return await _context.Payments
                .Where(x => x.Date >= firstDayOfMonth && x.Date <= lastDatOfMonth)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<Payment>> GetPaymentsWithDate(DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Where(x => x.Date >= startDate && x.Date <= endDate)
                .ToListAsync();
        }
        public async Task<List<Payment>> GetPaymentsByBarberIdWithDate(int barberId, DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Where(x => x.BarberId == barberId && x.Date >= startDate && x.Date <= endDate)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<Payment>> GetOnCreditPaymentsWithDate(DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Where(x => x.PaymentMethod == PaymentMethods.OnCredit && x.Date >= startDate && x.Date <= endDate)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<Payment>> GetCashPaymentsWithDate(DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Where(x => x.PaymentMethod == PaymentMethods.Cash && x.Date >= startDate && x.Date <= endDate)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<List<Payment>> GetCreditCardPaymentsWithDate(DateTime startDate, DateTime endDate)
        {
            return await _context.Payments
                .Where(x => x.PaymentMethod == PaymentMethods.CreditCard && x.Date >= startDate && x.Date <= endDate)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }
        public async Task<Payment?> GetLastPaymentWithCustomer()
        {
            return await _context.Payments
                .Include(x=> x.Reservation)
                .Include(x=> x.Barber)
                .OrderByDescending(x => x.Id)
                .Take(1)
                .FirstOrDefaultAsync();
        }
    }
}
