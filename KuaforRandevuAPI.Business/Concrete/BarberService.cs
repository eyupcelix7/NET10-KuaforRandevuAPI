using AutoMapper;
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
        private readonly IMapper _mapper;
        public BarberService(IRepository<Barber> repository, IValidator<CreateBarberDto> createBarberValidator, IMapper mapper)
        {
            _repository = repository;
            _createBarberValidator = createBarberValidator;
            _mapper = mapper;
        }
        public void CreateBarber(CreateBarberDto dto)
        {
            var validationResult = _createBarberValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                _repository.Add(_mapper.Map<Barber>(dto));
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
