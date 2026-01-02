using FluentValidation;
using KuaforRandevuAPI.Dtos.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.ReservationRules
{
    public class UpdateReservationStatusValidator : AbstractValidator<UpdateReservationStatusDto>
    {
        public UpdateReservationStatusValidator()
        {
            RuleFor(x=> x.Status).NotNull().NotEmpty().WithMessage("Rezervasyon durumu boş olamaz.");
        }
    }
}
