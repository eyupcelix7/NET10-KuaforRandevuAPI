using KuaforRandevuAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.DataAccess.Repositories.Abstract
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetServicesByBarberId(int id);
    }
}
