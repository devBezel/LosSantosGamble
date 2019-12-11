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
        }
    }
}
