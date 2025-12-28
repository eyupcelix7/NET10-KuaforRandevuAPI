using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KuaforRandevuAPI.Business.Concrete
{
    public class BarberService : IBarberService
    {
        private readonly IRepository<Barber> _repository;
        private readonly IValidator<CreateBarberDto> _createBarberValidator;
        public BarberService(IRepository<Barber> repository, IValidator<CreateBarberDto> createBarberValidator)
        {
            _repository = repository;
            _createBarberValidator = createBarberValidator;
        }
        public void CreateBarber(CreateBarberDto dto)
        {
            var validationResult = _createBarberValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                _repository.Add(new Barber
                {
                    BarberName = dto.BarberName,
                    JobStartTime = dto.JobStartTime,
                    JobEndTime = dto.JobEndTime
                });
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
