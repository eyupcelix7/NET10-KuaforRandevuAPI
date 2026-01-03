using KuaforRandevuAPI.Entities.Enums.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Dtos.Payment
{
    public class UpdatePaymentMethodDto
    {
        public int Id { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
    }
}
