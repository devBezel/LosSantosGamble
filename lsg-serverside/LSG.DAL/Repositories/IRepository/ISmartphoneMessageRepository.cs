using LSG.DAL.Database.Models.SmartphoneModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface ISmartphoneMessageRepository : IGenericRepository
    {
        Task<List<SmartphoneMessageModel>> GetAllMessagesFromNumber(int number);
    }
}
