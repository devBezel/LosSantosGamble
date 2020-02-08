using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Utilities
{
    public static class Calculation
    {
        public static int CalculateTheNumberOfDays(DateTime dayOne, DateTime dayTwo)
        {
            return (dayOne - dayTwo).Days;
        }

        public static int GenerateFreeIdentifier(List<int> idList)
        {
            int freeID = 0;

            foreach (int id in idList)
            {
                if(id == freeID)
                {
                    freeID++;
                }

                if (id > freeID) return freeID;
            }


            Alt.Log($"[SYSTEM-ID] Wolne ID dla gracza {freeID}");
            return freeID;

            //int ids = 0;

            //foreach (IPlayer player in Alt.GetAllPlayers())
            //{
            //    player.GetData("account:id", out int playerId);
            //    if (playerId == ids)
            //    {
            //        ids += 1;
            //    }

            //    if (playerId > ids)
            //    {
            //        return ids;
            //    }
            //}
            //Alt.Log($"[SERVER] Nadano graczowi ID: {ids}");
            //return ids;
        }

        public static void AssignPlayerServerID(IPlayer player)
        {
            ICollection<IPlayer> players = Alt.GetAllPlayers();
            List<int> playersId = new List<int>();
            foreach (IPlayer plr in players)
            {
                if(plr.HasData("account:id"))
                {
                    plr.GetData("account:id", out int resultPlayerID);
                    playersId.Add(resultPlayerID);
                }
            }

            int freeID = GenerateFreeIdentifier(playersId);

            Alt.Log($"[SYSTEM-ID] Nadałem graczowi {player.Name} ID: {freeID}");
            player.SetData("account:id", freeID);
        }
    }
}
