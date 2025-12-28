using FluentValidation;
using KuaforRandevuAPI.Dtos.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.ServiceRules
{
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceDto>
    {
        public UpdateServiceValidator()
        {
            RuleFor(x => x.ServiceName).NotEmpty().WithMessage("Ad kısımı boş olamaz");
            RuleFor(x => x.ServicePrice).NotEmpty().WithMessage("Ücret kısımı boş olamaz");
            RuleFor(x => x.ServiceDuration).NotEmpty().WithMessage("Süre kısımı boş olamaz");
        }
    }
}
