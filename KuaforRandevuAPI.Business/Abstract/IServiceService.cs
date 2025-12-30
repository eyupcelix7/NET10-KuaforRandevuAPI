using KuaforRandevuAPI.Common.Responses;
using KuaforRandevuAPI.Dtos.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Abstract
{
    public interface IServiceService
    {
        Task<ApiResponse<List<ResultServiceDto>>> GetAllService();
        Task<ApiResponse<ResultServiceDto>> GetServiceById(int id);
        Task<ApiResponse<List<ResultServiceDto>>> GetServicesByBarberId(int id);
        Task<ApiResponse<CreateServiceDto>> CreateService(CreateServiceDto dto);
        Task<ApiResponse<UpdateServiceDto>> UpdateService(UpdateServiceDto dto);
        Task<ApiResponse<int>> RemoveService(int id);
    }
}
