using LSG.DAL.Database.Models;
using LSG.DAL.Database.Models.CharacterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface ICharacterRepository : IGenericRepository
    {
        Task<IEnumerable<Character>> GetAccountCharacters(int id);
        Task<IEnumerable<CharacterDescription>> GetCharacterDescriptions(int id);
        Task<CharacterDescription> GetCharacterDescription(int id);
    }
}
