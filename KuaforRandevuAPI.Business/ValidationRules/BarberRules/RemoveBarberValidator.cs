using FluentValidation;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.BarberRules
{
    public class RemoveBarberValidator:AbstractValidator<RemoveBarberDto>
    {
        private readonly IRepository<Barber> _repository;
        public RemoveBarberValidator(IRepository<Barber> repository)
        {
            _repository = repository;
            RuleFor(x => x.Id).NotNull().WithMessage("Id boş olamaz");
            RuleFor(x=> x.Id).MustAsync(CheckBarber).WithMessage("Böyle bir berber bulunamadı.");
        }
        private async Task<bool> CheckBarber(int arg1, CancellationToken token)
        {
            var barber = await _repository.GetById(arg1);
            if (barber != null)
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
