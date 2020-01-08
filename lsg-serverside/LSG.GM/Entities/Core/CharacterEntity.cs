using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.BLL.Dto.Character;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core
{
    public static class CharacterEntity
    {
        public static Character GetCharacterEntity(this IPlayer player)
        {
            player.GetData("character:data", out Character character);

            return character;
        }


        public static void SendCharacterDataToClient(this IPlayer player)
        {
            player.GetData("character:data", out Character character);

            CharacterForListDto characterDto = Singleton.AutoMapper().Map<CharacterForListDto>(character);
            player.Emit("character:sendDataCharacter", characterDto);
        }

        public static void UpdateCharacterData(this IPlayer player, Character character)
        {
            player.SetData("character:data", character);

            Singleton.GetDatabaseInstance().Update(character);
            Singleton.GetDatabaseInstance().SaveChanges();

            player.SendCharacterDataToClient();
        }
    }
}
