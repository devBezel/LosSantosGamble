using AutoMapper;
using LSG.BLL.Dto.Account;
using LSG.BLL.Dto.Character;
using LSG.BLL.Dto.Vehicle;
using LSG.DAL.Database.Models;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.VehicleModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Konto
            CreateMap<Account, AccountForCharacterDto>();

            // Postacie
            CreateMap<Character, CharacterForListDto>();

            // Pojazdy
            CreateMap<Vehicle, VehicleToCharacterDto>();

            //Opisy postaci
            CreateMap<CharacterDescription, CharacterDescriptionForScriptDto>()
                .ForMember(d => d.CharacterId, opt => opt.MapFrom(c => c.Character.Id));
            CreateMap<CharacterDescriptionForScriptDto, CharacterDescription>();



            //Wygląd postaci
            CreateMap<CharacterLook, CharacterLookDto>();

        }
    }
}
