using AltV.Net;
using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace LSG.GM.Entities.Core.Item
{
    public class Food : ItemEntity
    {
        public float Caloricity => (float)DbModel.FirstParameter;

        public Food(ItemModel item) : base(item)
        {

        }

        public override void UseItem(CharacterEntity characterEntity)
        {
            if (characterEntity.Hunger >= 100)
            {
                characterEntity.AccountEntity.Player.SendWarningNotify("Jesteś najedzony!", "Twoja postać jest już najedzona");
                return;
            }

            if (DbModel.Count <= 0)
            {
                Remove();
            }

            DbModel.Count--;
            characterEntity.Hunger += Caloricity;

            Save();

            Timer timer = new Timer(4000);
            timer.Start();
            // Dorobić animacje i tekst na /me że spożywa jedzenie
        }
    }
}
