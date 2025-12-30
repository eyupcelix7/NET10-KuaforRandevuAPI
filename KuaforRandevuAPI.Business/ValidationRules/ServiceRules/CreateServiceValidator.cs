using FluentValidation;
using KuaforRandevuAPI.Dtos.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.ServiceRules
{
    public class CreateServiceValidator: AbstractValidator<CreateServiceDto>
    {
        public CreateServiceValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad kısımı boş olamaz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Ücret kısımı boş olamaz");
            RuleFor(x => x.Duration).NotEmpty().WithMessage("Süre kısımı boş olamaz");
            RuleFor(x => x.BarberId).NotEmpty().WithMessage("Berber kısımı boş olamaz");
        }
    }
}
