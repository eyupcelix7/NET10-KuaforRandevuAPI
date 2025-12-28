using AutoMapper;
using KuaforRandevuAPI.Business.Abstract;
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
        public ServiceService(IMapper mapper, IRepository<Service> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task CreateService(CreateServiceDto dto)
        {
            await _repository.Add(_mapper.Map<Service>(dto));
        }

        public Task<List<ResultServiceDto>> GetAllService()
        {
            throw new NotImplementedException();
        }

        public Task<ResultServiceDto> GetServiceById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateService(UpdateServiceDto dto)
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
        public async Task RemoveService(int id)
        {
            var service = await _repository.GetById(id);
            _repository.Remove(service);
        }
    }
}
