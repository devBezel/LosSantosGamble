using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core.Item.Scripts
{
    public class ObiectScript : IScript
    {
        [ClientEvent("item:cancelUseObject")]
        public void CancelObjectUse(IPlayer player, int itemID)
        {
            ItemEntity itemToDelete = player.GetAccountEntity().characterEntity.ItemsInUse.FirstOrDefault(item => item.Id == itemID);
            player.GetAccountEntity().characterEntity.ItemsInUse.Remove(itemToDelete);
        }
    }
}
