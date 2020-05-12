using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core.Warehouse;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Economy.Warehouses
{
    public class WarehouseScript : IScript
    {
        [ScriptEvent(ScriptEventType.ColShape)]
        public void OnEnterColshape(IColShape colShape, IEntity targetEntity, bool state)
        {
            IPlayer player = targetEntity as IPlayer;
            if (!state) return;

            //TODO: Zobaczyć czemu coś tu nie działa
            Alt.Log($"{colShape.HasData("warehouse:data")} warehouse:data");
            if (colShape == null || !colShape.Exists)
            {
                if (targetEntity.Type == BaseObjectType.Player)
                {
                    Alt.Log("Wchodze do colshape");
                    if(colShape.HasData("warehouse:data"))
                    {
                        Alt.Log("Colshape ma dane warehouse");
                        colShape.GetData("warehouse:data", out WarehouseEntity warehouseEntity);

                        if (warehouseEntity == null)
                            return;

                        Alt.Log($"Wszedłem w colshape magazynu: ${warehouseEntity.DbModel.Id}");
                    }
                }
            }
        }
    }
}
