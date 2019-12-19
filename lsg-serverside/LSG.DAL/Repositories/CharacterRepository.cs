using LSG.DAL.Database;
using LSG.DAL.Database.Models;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class CharacterRepository : GenericRepository, ICharacterRepository
    {
        private readonly RoleplayContext _context;

        public CharacterRepository(RoleplayContext context) : base (context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAccountCharacters(int id)
        {
            var characters = await _context.Characters.Where(x => x.Account.Id == id).Include(a => a.Account)
                .Include(v => v.Vehicles)
                .Include(d => d.CharacterDescriptions).ToListAsync();

            return characters;
        }

        public async Task<CharacterLook> GetCharacterLook(int id)
        {
            CharacterLook characterLook = await _context.CharacterLooks.SingleOrDefaultAsync(l => l.CharacterId == id);

            return characterLook;
        }

        public async Task<IEnumerable<CharacterDescription>> GetCharacterDescriptions(int id)
        {
            IEnumerable<CharacterDescription> characterDescriptions = await _context.CharacterDescriptions.Where(x => x.Character.Id == id).Include(c => c.Character).ToListAsync();

            return characterDescriptions;
        }

        public async Task<CharacterDescription> GetCharacterDescription(int id)
        {
            return await _context.CharacterDescriptions.SingleOrDefaultAsync(c => c.Id == id);
        }

    }
}
