using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Economy.Jobs.Base.Courier;
using LSG.GM.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Warehouse;
using LSG.GM.Entities.Job;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using LSG.GM.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

            if(worker.DbModel.JobEarned >= worker.CasualJob.JobEntityModel.MaxSalary)
            {
                player.SendChatMessageInfo("Zarobiłeś już maksymalną ilość pieniędzy. Przyjedź jutro");
                return;
            }
            
            if(!orderEntity.IsDelivered)
            {
                if(worker.CurrentDeliveryOrder == null)
                {
                    orderEntity.StartDeliviery(worker);

                    
                    player.CreateDrawText(new DrawTextModel()
                    {
                        Text = "Kliknij ~g~E ~w~ aby odlożyć paczkę",
                        X = orderEntity.DbModel.Warehouse.PosX,
                        Y = orderEntity.DbModel.Warehouse.PosY,
                        Z = orderEntity.DbModel.Warehouse.PosZ,
                        Dimension = 0,
                        UniqueID = $"WAREHOUSE_ORDER_DRAW_TEXT{orderEntity.DbModel.Id}"
                    });

                    Task.Run(async () =>
                    {
                        await player.CreateBlip(new BlipModel()
                        {
                            Name = $"Paczka ({orderEntity.DbModel.Name})",
                            Blip = 478,
                            Color = 76,
                            PosX = orderEntity.DbModel.Warehouse.PosX,
                            PosY = orderEntity.DbModel.Warehouse.PosY,
                            PosZ = orderEntity.DbModel.Warehouse.PosZ,
                            ShortRange = false, 
                            Size = EBlipSize.Medium,
                            UniqueID = $"WAREHOUSE_ORDER_BLIP{orderEntity.DbModel.Id}"
                        });
                    });

                    player.CallNative("addPointToGpsCustomRoute", new object[] { orderEntity.DbModel.Warehouse.PosX, orderEntity.DbModel.Warehouse.PosY, orderEntity.DbModel.Warehouse.PosZ });
                    player.CallNative("setGpsMultiRouteRender", new object[] { true });

                    player.SendChatMessageInfo("Twoja paczka została zapakowana do wozu, miejsce dostarczenia zostało oznaczone na mapie.");
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
                Task.Run(async () =>
                {
                    await player.DeleteBlip($"WAREHOUSE_ORDER_BLIP{entityOrder.DbModel.Id}");
                });
                player.CallNative("clearGpsMultiRoute");

                entityOrder.Delivier(characterEntity);

                player.SendChatMessageInfo("Dostarczyłeś paczkę pomyślnie!");
                
                characterEntity.CasualJob.ChargeMoney(characterEntity, 20);
                player.SendChatMessageInfo("Otrzymałeś 20$ za dostarczoną przesyłkę");
            }

        }
    }   
}
