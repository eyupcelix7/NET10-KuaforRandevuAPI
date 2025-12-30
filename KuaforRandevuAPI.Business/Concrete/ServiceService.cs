using AutoMapper;
using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Business.ValidationRules.ServiceRules;
using KuaforRandevuAPI.Common.Responses;
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
        public async Task<ApiResponse<List<ResultServiceDto>>> GetAllService() 
        {
            var services = await _repository.GetAll();
            var data = _mapper.Map<List<ResultServiceDto>>(services);
            return ApiResponse<List<ResultServiceDto>>.SuccessResponse(data);
        }
        public async Task<ApiResponse<ResultServiceDto>> GetServiceById(int id)
        {
            var service = await _repository.GetById(id);
            var data = _mapper.Map<ResultServiceDto>(service);
            return ApiResponse<ResultServiceDto>.SuccessResponse(data);
        }
        public async Task<ApiResponse<List<ResultServiceDto>>> GetServicesByBarberId(int id)
        {
            var values = await _serviceRepository.GetServicesByBarberId(id);
            var data = _mapper.Map<List<ResultServiceDto>>(values);
            return ApiResponse<List<ResultServiceDto>>.SuccessResponse(data);
        }
        public async Task<ApiResponse<CreateServiceDto>> CreateService(CreateServiceDto dto)
        {
            var validationResult = _createServiceValidator.Validate(dto);

            if (validationResult.IsValid)
            {
                var data = _mapper.Map<Service>(dto);
                await _repository.Add(data);
                return ApiResponse<CreateServiceDto>.SuccessResponse(dto, "OK");
            }
            else
            {
                return ApiResponse<CreateServiceDto>.ErrorResponse("Validation Error",validationResult.Errors.Select(x=> x.ErrorMessage));
            }
        }
        public async Task<ApiResponse<UpdateServiceDto>> UpdateService(UpdateServiceDto dto)
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
                    return ApiResponse<UpdateServiceDto>.SuccessResponse(dto, "OK");
                }
                return ApiResponse<UpdateServiceDto>.ErrorResponse("Not Found", null ,404);
            }
            else
            {
                return ApiResponse<UpdateServiceDto>.ErrorResponse("Validation Error", validationResult.Errors.Select(x => x.ErrorMessage));
            }
        }
        public async Task<ApiResponse<int>> RemoveService(int id)
        {
            var service = await _repository.GetById(id);
            if(service != null)
            {
                await _repository.Remove(service);
                return ApiResponse<int>.SuccessResponse(id,"OK");
            }
            return ApiResponse<int>.ErrorResponse("Not Found", null, 404);
        }
    }
}
