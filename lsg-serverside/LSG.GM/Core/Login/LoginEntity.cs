//using AltV.Net;
//using AltV.Net.Elements.Entities;
//using LSG.DAL.Database.Models.CharacterModels;
//using LSG.GM.Utilities;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace LSG.GM.Core.Login
//{
//    public static class LoginEntity
//    {

//        public static void SetPlayerDataToServer(IPlayer player, Character character)
//        {
//            //Jak będzie trzeba dodać np pojazdy aktualne gracza to trzeba je includować w select z bazy
//            Character characterDatabase = Singleton.GetDatabaseInstance().Characters.Include(a => a.Account).ThenInclude(p => p.AccountPremium).FirstOrDefault(c => c.Id == character.Id);

//            player.SetData("account:data", characterDatabase.Account);
//            player.SetData("character:data", characterDatabase);

//            player.SetData("account:id", Calculation.GenerateFreeIdentifier());
//            player.SetData("character:spawnedVehicleLength", 0);
//        }
//    }
//}
