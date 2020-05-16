using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Warehouse;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Economy.Warehouses
{
    public class WarehouseScript : IScript
    {
        [ScriptEvent(ScriptEventType.ColShape)]
        public void OnEnterColshape(IColShape colShape, IEntity entity, bool state)
        {
            if (!state) return;

            if (colShape.HasData("warehouse:data"))
            {
                Alt.Log("Przechodze po HasData");
                if (entity is IPlayer player)
                {
                    Alt.Log("Colshape ma dane warehouse");
                    colShape.GetData("warehouse:data", out WarehouseEntityOrder warehouseEntity);

                    if (warehouseEntity == null)
                        return;

                    CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

                    if(characterEntity.CurrentDeliveryOrder != null)
                    {
                        new Interaction(player, "warehouse:deliverOrder", "aby dostarczyć paczkę");                   
                    }
                }

            }
        }
    }
}
