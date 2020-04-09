using LSG.DAL.Database.Models.BusModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface IBusRepository : IGenericRepository
    {
        Task<List<BusStop>> GetAll();
    }
}
