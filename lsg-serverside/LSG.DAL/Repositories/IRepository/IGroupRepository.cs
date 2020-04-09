using LSG.DAL.Database.Models.GroupModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface IGroupRepository : IGenericRepository
    {
        Task<List<GroupModel>> GetAll();
    }
}
