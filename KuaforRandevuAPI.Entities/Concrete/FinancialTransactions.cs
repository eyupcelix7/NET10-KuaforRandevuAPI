using KuaforRandevuAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Concrete
{
    public class FinancialTransactions:IEntity
    {
        // Gelir Gider işlemleri (Örn: Faturlar)
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public Barber? Barber { get; set; }
        public int BarberId { get; set; }
    }
}
