using LSG.BLL.Dto.Character;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.BLL.Services.Interfaces
{
    public interface ICharacterService : IDisposable
    {
        Task<IEnumerable<CharacterForListDto>> GetAccountCharacters(int id);
        new void Dispose();
    }
}
