using LSG.DAL.Database.Models.BankModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface IAtmRepository : IGenericRepository
    {
        Task<List<Atm>> GetAll();
    }
}
