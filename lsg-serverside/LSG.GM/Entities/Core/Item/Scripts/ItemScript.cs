using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core.Item.Scripts
{
    public class ItemScript : IScript
    {

        ItemEntityFactory ItemFactory { get; } = new ItemEntityFactory();

        [Command("itemuse")]
        public void ItemUseTestCMD(IPlayer sender, int index)
        {
            CharacterEntity characterEntity = sender.GetAccountEntity().characterEntity;

            ItemEntity itemEntity = ItemFactory.Create(characterEntity.DbModel.CharacterItems.FirstOrDefault(x => x.Id == index));
            Alt.Log("ilość rzeczy w ekwipunku: " + characterEntity.DbModel.CharacterItems.Count().ToString());
            itemEntity.UseItem(characterEntity);
        }
    }
}
