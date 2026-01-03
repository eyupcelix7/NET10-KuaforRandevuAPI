using KuaforRandevuAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Concrete
{
    public class StockTransactions: IEntity
    {
        // Stok - Envanter işlemleri
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public Barber? Barber { get; set; }
        public int BarberId { get; set; }
    }
}
