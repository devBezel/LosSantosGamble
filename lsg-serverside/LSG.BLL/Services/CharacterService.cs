using AutoMapper;
using LSG.BLL.Dto.Character;
using LSG.BLL.Services.Interfaces;
using LSG.DAL.Database.Models;
using LSG.DAL.Database.Models.CharacterModels;
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

        public async Task<CharacterLookDto> GetCharacterLook(int characterId)
        {
            CharacterLook characterLook = await _unitOfWork.CharacterRepository.GetCharacterLook(characterId);

            CharacterLookDto characterLookToReturn = _mapper.Map<CharacterLookDto>(characterLook);

            return characterLookToReturn;

        }

        public async Task<IEnumerable<CharacterDescriptionForScriptDto>> GetCharacterDescriptions(int id)
        {
            IEnumerable<CharacterDescription> characterDescriptions = await _unitOfWork.CharacterRepository.GetCharacterDescriptions(id);

            IEnumerable<CharacterDescriptionForScriptDto> characterDescriptionsToReturn = _mapper.Map<IEnumerable<CharacterDescriptionForScriptDto>>(characterDescriptions);

            return characterDescriptionsToReturn;
        }

        public async Task<CharacterDescriptionForScriptDto> CreateDescription(CharacterDescriptionForScriptDto entity)
        {
            if (entity.Title.Length > 20)
                entity.Title = entity.Title.Remove(20);
            CharacterDescription characterDescription = _mapper.Map<CharacterDescription>(entity);

            _unitOfWork.CharacterRepository.Add<CharacterDescription>(characterDescription);
            
            await _unitOfWork.CharacterRepository.SaveAll();

            entity.Id = characterDescription.Id;

            return entity;
        }

        public async Task<bool> DeleteDescription(int id)
        {
            CharacterDescription characterDescription = await _unitOfWork.CharacterRepository.GetCharacterDescription(id);
            _unitOfWork.CharacterRepository.Delete<CharacterDescription>(characterDescription);

            if (await _unitOfWork.CharacterRepository.SaveAll())
                return true;

            return false;
        }

        public async Task<bool> SaveCharacterLook(int id, CharacterLookDto characterLookDto)
        {
            CharacterLook characterLook = _mapper.Map<CharacterLook>(characterLookDto);
            characterLook.CharacterId = id;

            if (!await _unitOfWork.CharacterRepository.SaveCharacterLook(id, characterLook))
                return false;

            return true;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}
