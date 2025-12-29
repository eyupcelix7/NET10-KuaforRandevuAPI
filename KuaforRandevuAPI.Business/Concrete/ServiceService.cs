using AutoMapper;
using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Business.ValidationRules.ServiceRules;
using KuaforRandevuAPI.DataAccess.Repositories.Abstract;
using KuaforRandevuAPI.Dtos.Services;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Concrete
{
    public class ServiceService : IServiceService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Service> _repository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IValidator<CreateServiceDto> _createServiceValidator;
        private readonly IValidator<UpdateServiceDto> _updateServiceValidator;
        public ServiceService(IMapper mapper, IRepository<Service> repository, IValidator<CreateServiceDto> createServiceValidator, IValidator<UpdateServiceDto> updateServiceValidator, IServiceRepository serviceRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _createServiceValidator = createServiceValidator;
            _updateServiceValidator = updateServiceValidator;
            _serviceRepository = serviceRepository;
        }
        public async Task<List<ResultServiceDto>> GetAllService()
        {
            var services = await _repository.GetAll();
            return _mapper.Map<List<ResultServiceDto>>(services);
        }
        public async Task<ResultServiceDto> GetServiceById(int id)
        {
            var service = await _repository.GetById(id);
            return _mapper.Map<ResultServiceDto>(service);
        }
        public async Task<List<ResultServiceDto>> GetServicesByBarberId(int id)
        {
            var values = await _serviceRepository.GetServicesByBarberId(id);
            return _mapper.Map<List<ResultServiceDto>>(values);
        }
        public async Task CreateService(CreateServiceDto dto)
        {
            var validationResult = _createServiceValidator.Validate(dto);

            if (validationResult.IsValid)
            {
                await _repository.Add(_mapper.Map<Service>(dto));
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
        public async Task UpdateService(UpdateServiceDto dto)
        {
            var validationResult = _updateServiceValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                var service = await _repository.GetById(dto.Id);
                if (service != null)
                {
                    service.ServiceName = dto.ServiceName;
                    service.ServicePrice = dto.ServicePrice;
                    service.ServiceDuration = dto.ServiceDuration;
                    await _repository.Update(service);
                }
            }
            else
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
        public async Task RemoveService(int id)
        {
            var service = await _repository.GetById(id);
            if(service != null)
            {
                await _repository.Remove(service);
            }
        }
    }
}
