using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.GM.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Economy.Money
{
    public static class MoneyEntity
    {
        public static void AddMoney(this IPlayer player, int amount)
        {
            Character character = player.GetCharacterEntity();
            character.Money += amount;

            player.UpdateCharacterData(character);
        }

        public static void RemoveMoney(this IPlayer player, int amount)
        {
            Character character = player.GetCharacterEntity();
            character.Money -= amount;

            player.UpdateCharacterData(character);
        }

        public static bool HasEnoughMoney(this IPlayer player, int amount)
        {
            Character character = player.GetCharacterEntity();

            return (character.Money >= amount) ? true : false;
        }
    }
}
