using FluentValidation;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Services;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.ServiceRules
{
    public class CreateServiceValidator: AbstractValidator<CreateServiceDto>
    {
        private readonly IRepository<Barber> _repository;
        public CreateServiceValidator(IRepository<Barber> repository)
        {
            _repository = repository;

            RuleFor(x => x.Name).NotNull().WithMessage("Ad boş olamaz.");
            RuleFor(x => x.Duration).NotNull().WithMessage("Süre boş olamaz.");
            RuleFor(x => x.BarberId).NotNull().WithMessage("Berber boş olamaz.");

            RuleFor(x => x.BarberId).MustAsync(CheckBarber).WithMessage("Böyle bir berber bulunamadı.");
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
    }
}
