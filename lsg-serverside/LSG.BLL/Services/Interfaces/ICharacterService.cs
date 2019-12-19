using LSG.BLL.Dto.Character;
using LSG.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.BLL.Services.Interfaces
{
    public interface ICharacterService : IDisposable
    {
        Task<IEnumerable<CharacterForListDto>> GetAccountCharacters(int id);
        Task<IEnumerable<CharacterDescriptionForScriptDto>> GetCharacterDescriptions(int id);
        Task<CharacterDescriptionForScriptDto> CreateDescription(CharacterDescriptionForScriptDto characterDescription);
        Task<CharacterLookDto> GetCharacterLook(int characterId);
        Task<bool> DeleteDescription(int id);
        new void Dispose();
    }
}
