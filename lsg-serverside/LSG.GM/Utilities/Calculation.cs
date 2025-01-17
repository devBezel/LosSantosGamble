﻿using AltV.Net;
using AltV.Net.Data;
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

        public static double Distance(Position posOne, Position posTwo)
        {
            return Math.Pow(posOne.X - posTwo.X, 2) + Math.Pow(posOne.Y - posTwo.Y, 2) + Math.Pow(posOne.Z - posTwo.Z, 2);
        }

        public static bool IsPlayerInRange(IPlayer sender, IPlayer getter, int range)
        {
            return Distance(sender.Position, getter.Position) <= range ? true : false;
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

        public static Position GetPositionInBackOfPosition(this Position pos, float rotation, float distance)
        {
            Position position = pos;
            float rot = rotation;
            var radius = rot * Math.PI / 180;
            Position newPos = new Position(position.X + (float)(distance * Math.Sin(-radius)), position.Y + (float)(distance * Math.Cos(-radius)), position.Z);
            return newPos;
        }
    }
}
