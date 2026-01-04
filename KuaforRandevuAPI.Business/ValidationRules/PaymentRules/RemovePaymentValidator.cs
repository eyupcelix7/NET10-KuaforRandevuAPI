using FluentValidation;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Payment;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.PaymentRules
{
    public class RemovePaymentValidator:AbstractValidator<RemovePaymentDto>
    {
        private readonly IRepository<Payment> _repository;
        public RemovePaymentValidator(IRepository<Payment> repository)
        {
            _repository = repository;
            RuleFor(x=> x.Id).NotNull().WithMessage("Id boş olamaz.");
            RuleFor(x => x.Id).MustAsync(CheckPayment).WithMessage("Böyle bir ödeme bulunamadı.");
        }
        private async Task<bool> CheckPayment(int arg1, CancellationToken token)
        {
            var payment = await _repository.GetById(arg1);
            if(payment != null)
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
