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
            RuleFor(x => x.BarberName).NotEmpty().WithMessage("Berber adı boş olamaz.");
            RuleFor(x => x.JobStartTime).NotEmpty().WithMessage("Mesai başlangıç saati boş olamaz.");
            RuleFor(x => x.JobEndTime).NotEmpty().WithMessage("Mesai bitiş saati boş olamaz.");
            RuleFor(x => x.JobStartTime).LessThan(x => x.JobEndTime).WithMessage("Mesai başlangıç saati, mesai bitiş saatinden önce olmalıdır.");
            RuleFor(x => x).Must(BeUniqueName).WithMessage("Bu ad soyad ile bir berber zaten mevcut.");
        }

        private bool BeUniqueName(UpdateBarberDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.BarberName))
            {
                return !_repository.checkNameExists(dto.BarberName,dto.Id );
            }
            else
            {
                return false;
            }
        }

    }
}
