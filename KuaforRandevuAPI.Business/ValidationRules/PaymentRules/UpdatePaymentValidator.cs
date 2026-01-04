using FluentValidation;
using KuaforRandevuAPI.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.PaymentRules
{
    public class UpdatePaymentValidator:AbstractValidator<UpdatePaymentDto>
    {
        public UpdatePaymentValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id boş olamaz.");
            RuleFor(x => x.BarberId).NotNull().WithMessage("Berber boş olamaz.");
            RuleFor(x => x.ReservationId).NotNull().WithMessage("Rezervasyon boş olamaz.");
            RuleFor(x => x.Date).NotNull().WithMessage("Tarih boş olamaz.");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Tutar 0'dan küçük olamaz.");
            RuleFor(x => x.PaymentMethod).NotNull().WithMessage("Ödeme yöntemi boş olamaz.");
        }
    }
}
