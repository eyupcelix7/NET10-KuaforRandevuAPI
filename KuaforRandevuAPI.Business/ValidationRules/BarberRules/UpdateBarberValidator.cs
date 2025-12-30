using FluentValidation;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Barber;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.BarberRules
{
    public class UpdateBarberValidator:AbstractValidator<UpdateBarberDto>
    {
        private readonly IBarberRepository _repository;
        public UpdateBarberValidator(IBarberRepository repository)
        {
            _repository = repository;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Berber adı boş olamaz.");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("Mesai başlangıç saati boş olamaz.");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("Mesai bitiş saati boş olamaz.");
            RuleFor(x => x.StartTime).LessThan(x => x.EndTime).WithMessage("Mesai başlangıç saati, mesai bitiş saatinden önce olmalıdır.");
            RuleFor(x => x).Must(BeUniqueName).WithMessage("Bu ad soyad ile bir berber zaten mevcut.");
        }

        private bool BeUniqueName(UpdateBarberDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                return !_repository.checkNameExists(dto.Name,dto.Id );
            }
            else
            {
                return false;
            }
        }

    }
}
