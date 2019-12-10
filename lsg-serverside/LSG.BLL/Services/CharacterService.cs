using AutoMapper;
using LSG.BLL.Dto.Character;
using LSG.BLL.Services.Interfaces;
using LSG.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.BLL.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CharacterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CharacterForListDto>> GetAccountCharacters(int id)
        {
            var characters = await _unitOfWork.CharacterRepository.GetAccountCharacters(id);

            var characterToReturn = _mapper.Map<IEnumerable<CharacterForListDto>>(characters);

            return characterToReturn;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
