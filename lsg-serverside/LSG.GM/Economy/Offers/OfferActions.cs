using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.DAL.UnitOfWork;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Vehicle;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using LSG.GM.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Economy.Offers
{
    public static class OfferActions
    {
        #region Paramedic
        public static void ResuscitationPlayerAction(CharacterEntity sender, CharacterEntity getter, object[] args)
        {
            if (!getter.HasBw)
            {
                sender.AccountEntity.Player.SendChatMessageError("Ten gracz żyje, nie możesz go reanimować");
                return;
            }

            sender.AccountEntity.Player.PlayAnimation("mini@cpr@char_a@cpr_str", "cpr_pumpchest", 6000);

            // Do sprawdzenia
            Task.Run(async () =>
            {
                await Task.Delay(6000);

                getter.UnBw();
                getter.AccountEntity.Player.SendChatMessageInfo($"Zostałeś uleczony przez {sender.DbModel.Name} {sender.DbModel.Surname}.");
            });
        }

        #endregion

        #region Mechanic

        public static void TuningPlayerVehicle(CharacterEntity sender, CharacterEntity getter, object[] args)
        {
            VehicleEntity vehicleToUpgrade = (VehicleEntity)args[0];
            ItemModel itemUpgrade = (ItemModel)args[1];
            bool IsStunned = (bool)args[2];

            Alt.Log($"{IsStunned} isStunned");
            byte category = 0;
            byte variation = 0;

            if (IsStunned)
            {
                itemUpgrade.VehicleUpgradeId = null;
                itemUpgrade.CharacterId = getter.DbModel.Id;
            }
            else
            {
                category = (byte)itemUpgrade.SecondParameter;
                variation = (byte)itemUpgrade.ThirdParameter;

                itemUpgrade.VehicleUpgradeId = vehicleToUpgrade.DbModel.Id;
                itemUpgrade.CharacterId = null;
            }

            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unit = new UnitOfWork(ctx))
            {
                unit.ItemRepository.Update(itemUpgrade);
            }

            string actionInProgress = IsStunned ? "demontowana" : "montowana";
            sender.AccountEntity.Player.SendChatMessageInfo($"Część jest {actionInProgress}, odczekaj chwile...");

            Task.Run(async () =>
            {
                //TODO: zrobić animacje

                await Task.Delay(6000);

                await AltAsync.Do(() =>
                {
                    vehicleToUpgrade.GameVehicle.ModKit = 1;

                    if ((TuningType)itemUpgrade.FirstParameter == TuningType.Wheels)
                    {
                        vehicleToUpgrade.GameVehicle.SetWheels(category, variation);
                    }
                    else
                    {
                        vehicleToUpgrade.GameVehicle.SetMod(category, variation);
                    }
                    
                });
                string actionFinished = IsStunned ? "zdemontowana" : "zamontowana";
                sender.AccountEntity.Player.SendChatMessageInfo($"Część została {actionFinished}");
            });
        }

        #endregion
    }
}
