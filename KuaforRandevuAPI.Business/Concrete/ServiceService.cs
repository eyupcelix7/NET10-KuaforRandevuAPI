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
        private readonly IValidator<RemoveServiceDto> _removeServiceValidator;
        public ServiceService(IMapper mapper, IRepository<Service> repository, IValidator<CreateServiceDto> createServiceValidator, IValidator<UpdateServiceDto> updateServiceValidator, IServiceRepository serviceRepository, IValidator<RemoveServiceDto> removeServiceValidator)
        {
            _mapper = mapper;
            _repository = repository;
            _createServiceValidator = createServiceValidator;
            _updateServiceValidator = updateServiceValidator;
            _serviceRepository = serviceRepository;
            _removeServiceValidator = removeServiceValidator;
        }
        public async Task<ApiResponse<List<ResultServiceDto>>> GetAllService()
        {
            var services = await _repository.GetAll();
            var data = _mapper.Map<List<ResultServiceDto>>(services);
            return ApiResponse<List<ResultServiceDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<ResultServiceDto>> GetServiceById(int id)
        {
            var service = await _repository.GetById(id);
            var data = _mapper.Map<ResultServiceDto>(service);
            return ApiResponse<ResultServiceDto>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<List<ResultServiceDto>>> GetServicesByBarberId(int id)
        {
            var values = await _serviceRepository.GetServicesByBarberId(id);
            var data = _mapper.Map<List<ResultServiceDto>>(values);
            return ApiResponse<List<ResultServiceDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<CreateServiceDto>> CreateService(CreateServiceDto dto)
        {
            var validationResult = await _createServiceValidator.ValidateAsync(dto);
            if (validationResult.IsValid)
            {
                var data = _mapper.Map<Service>(dto);
                await _repository.Add(data);
                return ApiResponse<CreateServiceDto>.SuccessResponse(dto, "OK");
            }
            else
            {
                return ApiResponse<CreateServiceDto>.ErrorResponse("Validasyon Hatası", validationResult.Errors.Select(x => x.ErrorMessage));
            }
        }
        public async Task<ApiResponse<UpdateServiceDto>> UpdateService(UpdateServiceDto dto)
        {
            var validationResult = await _updateServiceValidator.ValidateAsync(dto);
            if (validationResult.IsValid)
            {
                var service = await _repository.GetById(dto.Id);
                service!.Name = dto.Name;
                service.Duration = dto.Duration;
                await _repository.Update(service);
                return ApiResponse<UpdateServiceDto>.SuccessResponse(dto, "OK");
            }
            else
            {
                return ApiResponse<UpdateServiceDto>.ErrorResponse("Validasyon Hatası", validationResult.Errors.Select(x => x.ErrorMessage));
            }
        }
        public async Task<ApiResponse<RemoveServiceDto>> RemoveService(int id)
        {
            RemoveServiceDto removeDto = new RemoveServiceDto { Id = id };
            var validationResult = await _removeServiceValidator.ValidateAsync(removeDto);
            if (validationResult.IsValid)
            {
                var service = await _repository.GetById(id);
                await _repository.Remove(service!);
                return ApiResponse<RemoveServiceDto>.SuccessResponse(removeDto, "OK");
            }
            else
            {
                return ApiResponse<RemoveServiceDto>.ErrorResponse("Validasyon Hatası", validationResult.Errors.Select(x => x.ErrorMessage));
            }
        }
    }
}
