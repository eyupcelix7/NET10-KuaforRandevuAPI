using FluentValidation;
using KuaforRandevuAPI.Dtos.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.ValidationRules.ReservationRules
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDto>
    {
        public CreateReservationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad kısmı boş olamaz.");
            RuleFor(x => x.Name).MinimumLength(3);

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası boş olamaz.");
            RuleFor(x => x.PhoneNumber).MinimumLength(11).MaximumLength(11).WithMessage("Telefon numarası 05xxxxxxxxx formatında olmalıdır.");

            RuleFor(x => x.Date).NotNull();
            RuleFor(x => x.Date).Must(DateCheck).WithMessage("Randevu tarihi bugünden erken olamaz.");
        }
        private bool DateCheck(DateOnly only) // Randevu tarihi geçmişten bir gün olamaz.
        {
            var compare = only.CompareTo(DateOnly.FromDateTime(DateTime.Now));
            if (compare == 0 || compare == 1)
            {
                return true;
            }
            return false;
        }
    }
}
