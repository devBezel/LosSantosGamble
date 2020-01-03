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

        public static int GenerateFreeIdentifier()
        {
            int ids = 0;

            foreach (IPlayer player in Alt.GetAllPlayers())
            {
                player.GetData("account:id", out int playerId);
                if (playerId == ids)
                {
                    ids += 1;
                }

                if (playerId > ids)
                {
                    return ids;
                }
            }
            Alt.Log($"[SERVER] Nadano graczowi ID: {ids - 1}");
            return ids - 1;
        }
    }
}
