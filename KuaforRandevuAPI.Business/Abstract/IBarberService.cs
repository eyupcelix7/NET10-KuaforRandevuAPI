using KuaforRandevuAPI.Common.Responses;
using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Abstract
{
    public interface IBarberService
    {
        Task<ApiResponse<List<ResultBarberDto>>> GetAllBarber();
        Task<ApiResponse<ResultBarberDto>> GetBarberById(int id);
        Task<ApiResponse<ResultBarberWithServicesDto>> GetBarberByIdWithServices(int id);
        Task<ApiResponse<CreateBarberDto>> CreateBarber(CreateBarberDto dto);
        Task<ApiResponse<UpdateBarberDto>> UpdateBarber(UpdateBarberDto dto);
        Task<ApiResponse<int>> RemoveBarber(int id);
    }
}
