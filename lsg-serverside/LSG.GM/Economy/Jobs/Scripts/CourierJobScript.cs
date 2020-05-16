using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Economy.Jobs.Base.Courier;
using LSG.GM.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Warehouse;
using LSG.GM.Entities.Job;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Economy.Jobs.Scripts
{
    public class CourierJobScript : IScript
    {
        [Command("paczki")]
        public void GetCurrentOrders(IPlayer player)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

            if(characterEntity.CasualJob != null)
            {
                if (characterEntity.CasualJob is CourierJob courierJob)
                {
                    player.Emit("job-courier:currentOrders", courierJob.GetGroupWarehouseOrders());
                } 
                else
                {
                    player.SendChatMessageError("Twoja praca nie może przeglądać obecnych paczek do dostarczenia!");
                }
            } 
            else
            {
                player.SendChatMessageError("Rozpocznij pracę, aby móc użyć tej komendy!");
            }
        }

        [ClientEvent("job-courier:startDelivery")]
        public void StartDelivieryOrder(IPlayer player, int orderId)
        {
            CharacterEntity worker = player.GetAccountEntity().characterEntity;

            WarehouseOrderEntity orderEntity = EntityHelper.GetWarehouseOrderById(orderId);
            if (orderEntity == null)
                return;
            
            if(!orderEntity.IsDelivered)
            {
                if(worker.CurrentDeliveryOrder == null)
                {
                    orderEntity.StartDeliviery(worker);

                    player.Emit("job-courier:drawDeliveryGps", orderEntity.DbModel.Warehouse.PosX, orderEntity.DbModel.Warehouse.PosY, orderEntity.DbModel.Warehouse.PosZ);
                    player.CreateDrawText(new DrawTextModel()
                    {
                        Text = "Kliknij ~g~E ~w~ aby odlożyć paczkę",
                        X = orderEntity.DbModel.Warehouse.PosX,
                        Y = orderEntity.DbModel.Warehouse.PosY,
                        Z = orderEntity.DbModel.Warehouse.PosZ,
                        Dimension = 0,
                        UniqueID = $"WAREHOUSE_ORDER_DRAW_TEXT{orderEntity.DbModel.Id}"
                    });
                }
                else
                {
                    player.SendChatMessageError("Dostarczasz już jakąś paczkę!");
                }
            } 
            else
            {
                player.SendChatMessageInfo("Ktoś już dostarcza tą paczkę do zamawiającego");
            }
        }

        [ClientEvent("warehouse:deliverOrder")]
        public void WarehouseDeliverOrder(IPlayer player)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (characterEntity == null) return;

            if (characterEntity.CurrentDeliveryOrder != null)
            {
                WarehouseOrderEntity entityOrder = characterEntity.CurrentDeliveryOrder;

                player.RemoveDrawText($"WAREHOUSE_ORDER_DRAW_TEXT{entityOrder.DbModel.Id}");
                entityOrder.Delivier(characterEntity);


                player.SendChatMessageInfo("Dostarczyłeś paczkę pomyślnie!");

                characterEntity.CasualJob.ChargeMoney(characterEntity, 20);
                player.SendChatMessageInfo("Otrzymałeś 20$ za dostarczoną przesyłkę");
            }

        }
    }   
}
