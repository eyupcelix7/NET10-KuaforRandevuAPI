using FluentValidation;
using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.BarberRules
{
    public class CreateBarberValidator : AbstractValidator<CreateBarberDto>
    {
        public CreateBarberValidator()
        {
            RuleFor(x => x.BarberName).NotEmpty().WithMessage("Berber adı boş olamaz.");
            RuleFor(x => x.JobStartTime).NotEmpty().WithMessage("Mesai başlangıç saati boş olamaz.");
            RuleFor(x => x.JobEndTime).NotEmpty().WithMessage("Mesai bitiş saati boş olamaz.");
        }
    }
}
