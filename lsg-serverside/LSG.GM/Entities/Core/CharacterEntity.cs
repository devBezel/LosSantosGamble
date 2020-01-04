using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.CharacterModels;
using System;
using System.Collections.Generic;
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
    }
}
