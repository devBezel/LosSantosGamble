using AutoMapper;
using LSG.BLL.Dto.Account;
using LSG.BLL.Dto.Character;
using LSG.BLL.Dto.Vehicle;
using LSG.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Account, AccountForCharacterDto>();
            CreateMap<Character, CharacterForListDto>();
            CreateMap<Vehicle, VehicleToCharacterDto>();
            CreateMap<CharacterDescription, CharacterDescriptionForScriptDto>()
                .ForMember(d => d.CharacterId, opt => opt.MapFrom(c => c.Character.Id));
            CreateMap<CharacterDescriptionForScriptDto, CharacterDescription>()
                .ForPath(c => c.Character.Id, opt => opt.MapFrom(c => c.CharacterId));
        }
    }
}
