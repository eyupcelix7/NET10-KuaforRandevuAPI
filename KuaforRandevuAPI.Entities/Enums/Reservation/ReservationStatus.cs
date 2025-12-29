using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Entities.Enums.Reservation
{
    /// <summary>
    /// Rezervasyon Durumları
    /// </summary>
    public enum ReservationStatus
    {
        /// <summary>
        /// Beklemede
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Kabul Edildi
        /// </summary>
        Confirmed = 2,

        /// <summary>
        /// İptal Edildi
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// Tamamlandı
        /// </summary>
        Completed = 4
    }
}
