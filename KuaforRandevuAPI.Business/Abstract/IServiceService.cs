using KuaforRandevuAPI.Dtos.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Abstract
{
    public interface IServiceService
    {
        Task<List<ResultServiceDto>> GetAllService();
        Task<ResultServiceDto> GetServiceById(int id);
        void CreateService(CreateServiceDto dto);
    }
}
