using KuaforRandevuAPI.Dtos.Barber;
using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Business.Abstract
{
    public interface IBarberService
    {
        Task<List<ResultBarberDto>> GetAllBarber();
        Task<ResultBarberDto> GetBarberById(int id);
        Task<ResultBarberWithServicesDto> GetBarberByIdWithServices(int id);
        Task CreateBarber(CreateBarberDto dto);
        Task UpdateBarber(UpdateBarberDto dto);
        Task RemoveBarber(int id);
    }
}
