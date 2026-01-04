using FluentValidation;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Services;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.ServiceRules
{
    public class RemoveServiceValidator : AbstractValidator<RemoveServiceDto>
    {
        private readonly IRepository<Service> _repository;
        public RemoveServiceValidator(IRepository<Service> repository)
        {
            _repository = repository;

            RuleFor(x => x.Id).NotNull().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Id).MustAsync(CheckService).WithMessage("Böyle bir hizmet bulunamadı.");
        }

        private async Task<bool> CheckService(int arg1, CancellationToken token)
        {
            Service? service = await _repository.GetById(arg1);
            if (service != null)
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
