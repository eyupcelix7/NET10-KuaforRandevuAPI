using FluentValidation;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.BarberRules
{
    public class UpdateBarberValidator:AbstractValidator<UpdateBarberDto>
    {
        private readonly IRepository<Barber> _repository;
        private readonly IBarberRepository _barberRepository;
        public UpdateBarberValidator(IRepository<Barber> repository, IBarberRepository barberRepository)
        {
            _repository = repository;
            _barberRepository = barberRepository;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Berber adı boş olamaz.");
            RuleFor(x => x.StartTime).NotEmpty().WithMessage("Mesai başlangıç saati boş olamaz.");
            RuleFor(x => x.EndTime).NotEmpty().WithMessage("Mesai bitiş saati boş olamaz.");
            RuleFor(x => x.StartTime).LessThan(x => x.EndTime).WithMessage("Mesai başlangıç saati, mesai bitiş saatinden önce olmalıdır.");

            RuleFor(x => x.Id).MustAsync(CheckBarber).WithMessage("Böyle bir berber bulunamadı.");
            RuleFor(x => x).Must(BeUniqueName).WithMessage("Bu ad soyad ile bir berber zaten mevcut.");
        }
        private async Task<bool> CheckBarber(int arg1, CancellationToken token)
        {
            var barber = await _repository.GetById(arg1);
            if(barber != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool BeUniqueName(UpdateBarberDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                return !_barberRepository.checkNameExists(dto.Name,dto.Id);
            }
            else
            {
                return false;
            }
        }
    }
}
