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
        void CreateBarber(CreateBarberDto dto);
        Task<ResultBarberDto> GetBarberByIdWithServices(int id);
    }
}
