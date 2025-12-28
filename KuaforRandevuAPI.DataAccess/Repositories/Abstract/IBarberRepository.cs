using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Abstract
{
    public interface IBarberRepository
    {
        bool checkNameExists(string barberName,int? barberId = null);
        Task<Barber?> GetBarberByIdWithServices(int id);
        Task<List<Barber?>> GetBarbersWithServices();
    }
}
