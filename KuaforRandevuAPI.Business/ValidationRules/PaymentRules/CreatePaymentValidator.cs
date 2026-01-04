using FluentValidation;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Payment;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.PaymentRules
{
    public class CreatePaymentValidator:AbstractValidator<CreatePaymentDto>
    {
        private readonly IRepository<Barber> _barberRepository;
        private readonly IRepository<Reservation> _reservationRepository;
        public CreatePaymentValidator(IRepository<Barber> barberRepository, IRepository<Reservation> reservationRepository)
        {
            _barberRepository = barberRepository;
            _reservationRepository = reservationRepository;

            RuleFor(x => x.BarberId)
                .NotNull()
                .WithMessage("Berber boş olamaz.");
            RuleFor(x => x.ReservationId)
                .NotNull()
                .WithMessage("Rezervasyon boş olamaz.");
            RuleFor(x => x.Date)
                .NotNull()
                .WithMessage("Tarih boş olamaz.");
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Tutar 0'dan büyük olmalıdır.");
            RuleFor(x => x.PaymentMethod)
                .NotNull()
                .WithMessage("Ödeme yöntemi boş olamaz.");

            RuleFor(x => x.BarberId)
                .MustAsync(CheckBarber)
                .WithMessage("Böyle bir berber bulunamadı.");
            RuleFor(x => x.ReservationId)
                .MustAsync(CheckReservation)
                .WithMessage("Böyle bir Rezervasyon bulunamadı.");
        }
        private async Task<bool> CheckBarber(int arg1, CancellationToken token)
        {
            var check = await _barberRepository.GetById(arg1);
            if(check != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private async Task<bool> CheckReservation(int arg1, CancellationToken token)
        {
            var check = await _reservationRepository.GetById(arg1);
            if(check  != null)
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
