using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.GM.Economy.Groups.Base;
using LSG.GM.Economy.Offers;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Entities.Core.Vehicle;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core.Item
{
    internal class CarPart : ItemEntity 
    {
        public TuningType TuningType => (TuningType)DbModel.FirstParameter;
        public int VehicleModCategory => (int)DbModel.SecondParameter;
        public int Index => (int)DbModel.ThirdParameter;
        public uint VehicleModel => (uint)DbModel.FourthParameter;


        public CarPart(ItemModel item) : base(item)
        {

        }
        //TODO: Dorobić, że osoba zarządzająca grupą może akceptować oferte tuningu pojazdu
        public override void UseItem(CharacterEntity characterEntity)
        {
            IPlayer player = characterEntity.AccountEntity.Player;
            GroupEntity group = characterEntity.OnDutyGroup;

            if (group == null)
            {
                player.SendChatMessageInfo("Nie masz uprawnień, aby montować części w samochodzie");
                return;
            }
                

            if (group.DbModel.GroupType != GroupType.Mechanic || ((Mechanic)group).CanPlayerTuningVehicle(characterEntity.AccountEntity))
            {
                if(DbModel.VehicleUpgradeId == null)
                {
                    if(player.Vehicle == null)
                    {
                        player.SendChatMessageInfo("Musisz być w samochodzie, aby móc zamontować część do samochodu");
                        return;
                    }

                    if(player.Vehicle.Model == VehicleModel)
                    {
                        VehicleEntity vehicleToUpgrade = player.Vehicle.GetVehicleEntity();
                        if (vehicleToUpgrade == null)
                        {
                            player.SendChatMessageError("Do tego pojazdu nie możesz zamontować części");
                            return;
                        }
                        if (TuningType == TuningType.Wheels)
                        {
                            Alt.Log("vehicleToUpgrade.GameVehicle.WheelVariation: " + vehicleToUpgrade.GameVehicle.WheelVariation);
                            if (vehicleToUpgrade.GameVehicle.WheelVariation != 0)
                            {
                                player.SendChatMessageError("Część z tej kategorii jest już zamontowana w tym pojeździe, odmontuj ją, aby móc wykonać tą akcje");
                                return;
                            }
                        }
                        else
                        {
                            if (vehicleToUpgrade.GameVehicle.GetMod((byte)VehicleModCategory) != 0)
                            {
                                player.SendChatMessageError("Część z tej kategorii jest już zamontowana w tym pojeździe, odmontuj ją, aby móc wykonać tą akcje");
                                return;
                            }
                        }

                        CharacterEntity ownerVehicle = PlayerExtenstion.GetPlayerByCharacterId(vehicleToUpgrade.DbModel.OwnerId);
                        if (ownerVehicle == null || !ownerVehicle.DbModel.Online)
                        {
                            player.SendChatMessageError("Ten gracz musi być w grze, abyś mógł zamontować część do jego pojazdu");
                            return;
                        }

                        OfferScript.OfferPlayer(player, "Montowanie części", ownerVehicle.AccountEntity.ServerID, OfferType.TuningVehicle, DbModel.Id, 100);
                    }
                    else
                    {
                        player.SendChatMessageError("Ta część nie pasuje do tego pojazdu!");
                    }
                }
                else
                {
                    player.SendChatMessageError("Ta część jest już zamontowana do pojazdu");
                }
            } 
            else
            {
                player.SendChatMessageError("Nie masz uprawnień do tego, aby móc zamontować część do samochodu! Zgłoś się do dyrektora swojej firmy, jeśli chcesz ubiegać się o te uprawnienie!");
            }
        }
    }
}
