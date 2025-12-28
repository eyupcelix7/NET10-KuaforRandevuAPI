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
        public void CreateService(CreateServiceDto dto)
        {
            _repository.Add(_mapper.Map<Service>(dto));
        }

        public Task<List<ResultServiceDto>> GetAllService()
        {
            throw new NotImplementedException();
        }

        public Task<ResultServiceDto> GetServiceById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
