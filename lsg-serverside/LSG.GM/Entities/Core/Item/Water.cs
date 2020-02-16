using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace LSG.GM.Entities.Core.Item
{
    public class Water : ItemEntity
    {
        public float Irrigation => (float)DbModel.FirstParameter;

        public Water(ItemModel item) : base(item)
        {

        }

        public override void UseItem(CharacterEntity characterEntity)
        {
            if (characterEntity.Thirsty >= 100)
            {
                characterEntity.AccountEntity.Player.SendWarningNotify("Jesteś nawodniony!", "Twoja postać jest już nawodniona");
                return;
            }

            if (DbModel.Count <= 0)
            {
                Remove();
            }

            DbModel.Count--;
            characterEntity.Thirsty += Irrigation;

            Save();

            Timer timer = new Timer(4000);
            timer.Start();
            // Dorobić animacje i tekst na /me że spożywa jedzenie
        }
    }
}
