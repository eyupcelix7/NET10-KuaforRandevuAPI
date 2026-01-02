using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Enums.PaymentMethods
{
    public enum PaymentMethods
    {
        /// <summary>
        /// Nakit
        /// </summary>
        Cash = 1,

        /// <summary>
        /// Kredi Kartı
        /// </summary>
        CreditCard = 2,

        /// <summary>
        /// Veresiye (Borç)
        /// </summary>
        OnCredit = 3
    }
}
