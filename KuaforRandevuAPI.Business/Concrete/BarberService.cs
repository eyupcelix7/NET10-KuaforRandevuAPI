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
        private readonly IBarberRepository _barberRepository;
        private readonly IValidator<CreateBarberDto> _createBarberValidator;
        private readonly IValidator<UpdateBarberDto> _updateBarberValidator;
        private readonly IMapper _mapper;
        public BarberService(IRepository<Barber> repository, IValidator<CreateBarberDto> createBarberValidator, IMapper mapper, IBarberRepository barberRepository, IValidator<UpdateBarberDto> updateBarberValidator)
        {
            _repository = repository;
            _createBarberValidator = createBarberValidator;
            _mapper = mapper;
            _barberRepository = barberRepository;
            _updateBarberValidator = updateBarberValidator;
        }
        public async Task<List<ResultBarberDto>> GetAllBarber()
        {
            var barbers = await _repository.GetAll();
            return _mapper.Map<List<ResultBarberDto>>(barbers);
        }
        public async Task<ResultBarberDto> GetBarberById(int id)
        {
            var barber = await _repository.GetById(id);
            return _mapper.Map<ResultBarberDto>(barber);
        }
        public async Task CreateBarber(CreateBarberDto dto)
        {
            var validationResult = _createBarberValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                await _repository.Add(_mapper.Map<Barber>(dto));
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
        public async Task<ResultBarberWithServicesDto> GetBarberByIdWithServices(int id)
        {
            var values = await _barberRepository.GetBarberByIdWithServices(id);
            return _mapper.Map<ResultBarberWithServicesDto>(values);
        }
        public async Task UpdateBarber(UpdateBarberDto dto)
        {
            var validationResult = _updateBarberValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var updatedBarber = await _repository.GetById(dto.Id);
                if (updatedBarber != null)
                {
                    updatedBarber.BarberName = dto.BarberName;
                    updatedBarber.JobStartTime = dto.JobStartTime;
                    updatedBarber.JobEndTime = dto.JobEndTime;
                    await _repository.Update(updatedBarber);
                }
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
        public async Task RemoveBarber(int id)
        {
            var barber = await _repository.GetById(id);
            if (barber != null)
            {
                await _repository.Remove(barber);
            }
        }
    }
}
