using LSG.DAL.Database;
using LSG.DAL.Database.Models.SmartphoneModels;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class SmartphoneMessageRepository : GenericRepository, ISmartphoneMessageRepository
    {
        private readonly RoleplayContext _context;
        public SmartphoneMessageRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<SmartphoneMessageModel>> GetAllMessagesFromNumber(int phoneNumber)
        {
            return await _context.SmartphoneMessages.Include(c => c.Cellphone).Where(x => x.GetterNumber == phoneNumber).ToListAsync();
        }
    }
}
