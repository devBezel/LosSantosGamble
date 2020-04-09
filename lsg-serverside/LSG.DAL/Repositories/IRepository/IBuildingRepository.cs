using LSG.DAL.Database.Models.BuildingModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface IBuildingRepository : IGenericRepository
    {
        Task<List<BuildingModel>> GetAll();
    }
}
