using AutoMapper;
using FluentValidation;
using KuaforRandevuAPI.Business.Abstract;
using KuaforRandevuAPI.Common.Responses;
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
        private readonly IValidator<RemoveBarberDto> _removeBarberValidator;
        private readonly IMapper _mapper;
        public BarberService(IRepository<Barber> repository, IValidator<CreateBarberDto> createBarberValidator, IMapper mapper, IBarberRepository barberRepository, IValidator<UpdateBarberDto> updateBarberValidator, IValidator<RemoveBarberDto> removeBarberValidator)
        {
            _repository = repository;
            _createBarberValidator = createBarberValidator;
            _mapper = mapper;
            _barberRepository = barberRepository;
            _updateBarberValidator = updateBarberValidator;
            _removeBarberValidator = removeBarberValidator;
        }
        public async Task<ApiResponse<List<ResultBarberDto>>> GetAllBarber()
        {
            var barbers = await _repository.GetAll();
            var data = _mapper.Map<List<ResultBarberDto>>(barbers);
            return ApiResponse<List<ResultBarberDto>>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<ResultBarberDto>> GetBarberById(int id)
        {
            var barber = await _repository.GetById(id);
            var data = _mapper.Map<ResultBarberDto>(barber);
            return ApiResponse<ResultBarberDto>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<ResultBarberWithServicesDto>> GetBarberByIdWithServices(int id)
        {
            var values = await _barberRepository.GetBarberByIdWithServices(id);
            var data = _mapper.Map<ResultBarberWithServicesDto>(values);
            return ApiResponse<ResultBarberWithServicesDto>.SuccessResponse(data, "OK");
        }
        public async Task<ApiResponse<CreateBarberDto>> CreateBarber(CreateBarberDto dto)
        {
            var validationResult = _createBarberValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                await _repository.Add(_mapper.Map<Barber>(dto));
                return ApiResponse<CreateBarberDto>.SuccessResponse(dto, "OK");
            }
            else
            {
                return ApiResponse<CreateBarberDto>.ErrorResponse("Validasyon Hatası", validationResult.Errors.Select(x=> x.ErrorMessage));
            }
        }
        public async Task<ApiResponse<UpdateBarberDto>> UpdateBarber(UpdateBarberDto dto)
        {
            var validationResult = await _updateBarberValidator.ValidateAsync(dto);
            if (validationResult.IsValid)
            {
                var updatedBarber = await _repository.GetById(dto.Id);
                updatedBarber!.Name = dto.Name;
                updatedBarber.StartTime = dto.StartTime;
                updatedBarber.EndTime = dto.EndTime;
                await _repository.Update(updatedBarber);
                return ApiResponse<UpdateBarberDto>.SuccessResponse(dto, "OK");
            }
            else
            {
                return ApiResponse<UpdateBarberDto>.ErrorResponse("Validasyon Hatası", validationResult.Errors.Select(x => x.ErrorMessage));
            }
        }
        public async Task<ApiResponse<RemoveBarberDto>> RemoveBarber(int id)
        {
            var removeDto = new RemoveBarberDto { Id = id };
            var validationResult = await _removeBarberValidator.ValidateAsync(removeDto);
            if (validationResult.IsValid)
            {
                var barber = await _repository.GetById(id);
                await _repository.Remove(barber!);
                return ApiResponse<RemoveBarberDto>.SuccessResponse(removeDto, "OK");
            }
            else
            {
                return ApiResponse<RemoveBarberDto>.ErrorResponse("Validasyon Hatası", validationResult.Errors.Select(x=> x.ErrorMessage));
            }
        }
    }
}
