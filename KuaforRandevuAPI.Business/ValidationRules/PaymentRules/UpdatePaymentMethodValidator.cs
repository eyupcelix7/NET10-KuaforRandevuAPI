using FluentValidation;
using KuaforRandevuAPI.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.PaymentRules
{
    public class UpdatePaymentMethodValidator:AbstractValidator<UpdatePaymentMethodDto>
    {
        public UpdatePaymentMethodValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id boş olamaz.");
            RuleFor(x => x.PaymentMethod).NotNull().WithMessage("Ödeme yöntemi boş olamaz.");
        }
    }
}
