using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.ItemModels;
using LSG.GM.Constant;
using LSG.GM.Entities.Core.Item;
using LSG.GM.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LSG.GM.Entities.Core.Buidling
{
    public class BuildingScript : IScript
    {
        public static ItemEntityFactory ItemFactory { get; } = new ItemEntityFactory();

        public BuildingScript()
        {
            AltAsync.OnColShape += OnEnterColshape;
            Alt.OnClient("building:enterBuilding", OnEnterBuilding);
            Alt.OnClient("building:exitBuilding", OnExitBuilding);
            Alt.OnClient("building:getManageData", GetBuildingManageData);
            Alt.OnClient("building:requestLockBuilding", RequestLockBuilding);
            Alt.OnClient("building:editData", BuildingEditData);
            AltAsync.OnClient("building:editOnSaleData", BuildingEditOnSaleData);
            Alt.OnClient("building:withdrawBalance", BuildingWithdrawBalance);
            Alt.OnClient("building:insertItemToMagazine", BuildingInsertItemToMagazine);
            Alt.OnClient("building:insertItemFromMagazineToEquipment", BuildingInsertItemFromMagazineToEquipment);
            Alt.OnClient("building:turnSbOut", BuildingPlayerTurnSbOut);
        }

        private async Task OnEnterColshape(IColShape colShape, IEntity targetEntity, bool state) => await AltAsync.Do(() =>
        {
            if (!state) return;

            if (colShape == null || !colShape.Exists) return;
            if (targetEntity.Type != BaseObjectType.Player) return;
            Alt.Log("Budynki");

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();

            Alt.Log("Pozycja exterioru: "  + buildingEntity.ExteriorColshape.Position.ToString());

            if (buildingEntity == null) return;

            IPlayer player = targetEntity as IPlayer;

            // Wejście do budynku lub wyjście
            if (buildingEntity.InteriorColshape == colShape || buildingEntity.ExteriorColshape == colShape)
            {
                bool colshapeState = buildingEntity.InteriorColshape == colShape ? true : false;

                player.EmitAsync("building:request", buildingEntity.DbModel.EntryFee, buildingEntity.DbModel.Name, colshapeState, buildingEntity.IsCharacterOwner(player));
                player.SetData("current:doors", colShape);
            }

            
        });

        public void OnEnterBuilding(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();

            if(buildingEntity.IsLocked)
            {
                player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Drzwi są zamknięte", "~g~Budynek", "Drzwi od tego budynku są zamknięte");
                return;
            }

            if(buildingEntity.DbModel.EntryFee > 0 && !buildingEntity.IsCharacterOwner(player))
            {
                if(!player.GetAccountEntity().characterEntity.HasEnoughMoney(buildingEntity.DbModel.EntryFee))
                {
                    player.SendNativeNotify(null, NotificationNativeType.Normal, 1, "Nie masz przy sobie gotówki", "~g~Budynek", $"Aby wejść do tego budynku potrzebujesz ~g~{buildingEntity.DbModel.EntryFee}$");
                    return;
                }

                player.GetAccountEntity().characterEntity.RemoveMoney(buildingEntity.DbModel.EntryFee);
                buildingEntity.AddMoney(buildingEntity.DbModel.EntryFee);

                player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Pobrano opłatę", "~g~Budynek", $"Pobrano opłatę {buildingEntity.DbModel.EntryFee}$ za wejście do budynku", 1);
            }

            // W późniejszym czasie wczytywanie customowych obiektów - dorobić tabelę building_objects i tam umieszczać obiekty

            player.GetAccountEntity().characterEntity.Dimension = buildingEntity.DbModel.Id;

            player.Position = new Position(buildingEntity.DbModel.ExternalPickupPositionX, buildingEntity.DbModel.ExternalPickupPositionY, buildingEntity.DbModel.ExternalPickupPositionZ);
            buildingEntity.PlayersInBuilding.Add(player);
        }

        public void OnExitBuilding(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();

            if (buildingEntity.IsLocked)
            {
                player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Drzwi są zamknięte", "~g~Budynek", "Drzwi od tego budynku są zamknięte");
                return;
            }

            player.GetAccountEntity().characterEntity.Dimension = 0; // Defaultowy świat graczy
            player.Position = new Position(buildingEntity.DbModel.InternalPickupPositionX, buildingEntity.DbModel.InternalPickupPositionY, buildingEntity.DbModel.InternalPickupPositionZ);

            buildingEntity.PlayersInBuilding.Remove(player);

            player.DeleteData("current:doors");
        }

        private void GetBuildingManageData(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();
            if (!buildingEntity.IsCharacterOwner(player)) return;

            player.Emit("building:manageData", buildingEntity.DbModel, buildingEntity.DbModel.ItemsInBuilding, player.GetAccountEntity().characterEntity.DbModel.Items, buildingEntity.PlayersInBuilding);

        }

        private void RequestLockBuilding(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();
            if (!buildingEntity.IsCharacterOwner(player)) return;
            Alt.Log("Przeszlo Lock pomyslnie");
            if(buildingEntity.IsLocked)
            {
                buildingEntity.IsLocked = false;
                player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Otworzyłeś budynek", "~g~Budynek", "Ten budynek został otwarty");
                

            } else
            {
                buildingEntity.IsLocked = true;
                player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Zamknąłeś budynek", "~g~Budynek", "Ten budynek został zamknięty");
            }

            Timer timer = new Timer(4000);
            timer.Start();
        }

        private void BuildingEditData(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();
            if (!buildingEntity.IsCharacterOwner(player)) return;

            string newBuildingName = args[0].ToString();
            int newEntryFee = Convert.ToInt32(args[1]);

            Alt.Log($"name: {newBuildingName} entryfee: {newEntryFee}");

            buildingEntity.DbModel.Name = newBuildingName;
            buildingEntity.DbModel.EntryFee = newEntryFee;

            player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Edycja budynku", "~g~Budynek", "Edycja budynku przebiegła pomyślnie", 1);
        }

        private async Task BuildingEditOnSaleData(IPlayer player, object[] args) => await AltAsync.Do(async () =>
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();
            if (!buildingEntity.IsCharacterOwner(player)) return;

            bool onSale = (bool)args[0];
            int saleCost = Convert.ToInt32(args[1]);

            buildingEntity.DbModel.OnSale = onSale;
            buildingEntity.DbModel.SaleCost = saleCost;

            if (!onSale) return;

            await buildingEntity.UpdateBlip();
            player.SendNativeNotify(null, NotificationNativeType.Normal, 1, "Wystawiłeś budynek na sprzedaż", "~g~Budynek", $"Twój budynek został wystawiony na sprzedaż za ~g~{saleCost}$", 1);
        });
        private void BuildingWithdrawBalance(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();
            if (!buildingEntity.IsCharacterOwner(player)) return;

            int toWithdraw = Convert.ToInt32(args[0]);

            if(!buildingEntity.HasEnoughMoney(toWithdraw))
            {
                player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Brak odpowiedniej kwoty w saldzie", "~g~Budynek", "W twoim saldzie znajduje się zbyt mało pieniędzy", 1);
                return;
            }

            buildingEntity.RemoveMoney(toWithdraw);
            player.GetAccountEntity().characterEntity.AddMoney(toWithdraw);

            player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Wypłaciłeś środki", "~g~Budynek", $"Wypłaciłeś z salda budynku ~g~{toWithdraw}$", 1);
        }

        public void BuildingInsertItemToMagazine(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();
            if (!buildingEntity.IsCharacterOwner(player)) return;

            int itemID = (int)(long)args[0];


            ItemModel itemToChange = player.GetAccountEntity().characterEntity.DbModel.Items.FirstOrDefault(item => item.Id == itemID);
            if (itemToChange == null) return;
            if (itemToChange.ItemInUse) return;

            itemToChange.CharacterId = null;
            itemToChange.BuildingId = buildingEntity.DbModel.Id;

            Alt.Log($"Po wlozeniu: {itemToChange.BuildingId}");
            player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Włożyłeś przedmiot do magazynu", "~g~Budynek", $"Włożyłeś {itemToChange.Name} do magazynu tego budynku");

            // Zrobić zapis
            ItemEntity itemEntity = ItemFactory.Create(itemToChange);
            itemEntity.Save();
        }

        private void BuildingInsertItemFromMagazineToEquipment(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();
            if (!buildingEntity.IsCharacterOwner(player)) return;

            int itemID = (int)(long)args[0];


            ItemModel itemToChange = buildingEntity.DbModel.ItemsInBuilding.FirstOrDefault(item => item.Id == itemID);
            if (itemToChange == null) return;

            itemToChange.ItemInUse = false;
            itemToChange.CharacterId = player.GetAccountEntity().characterEntity.DbModel.Id;
            itemToChange.BuildingId = null;

            Alt.Log($"Po wyciągnięciu: {itemToChange.CharacterId}");
            player.SendNativeNotify(null, NotificationNativeType.Building, 1, "Wyjąłeś przedmiot z magazynu", "~g~Budynek", $"Wyjąłeś {itemToChange.Name} z magazynu tego budynku");

            ItemEntity itemEntity = ItemFactory.Create(itemToChange);
            itemEntity.Save();
        }

        private void BuildingPlayerTurnSbOut(IPlayer player, object[] args)
        {
            player.GetData("current:doors", out IColShape colShape);
            if (colShape == null) return;

            BuildingEntity buildingEntity = colShape.GetBuildingEntity();
            if (!buildingEntity.IsCharacterOwner(player)) return;

            int getterId = (int)(long)args[0];
            IPlayer getter = PlayerExtenstion.GetPlayerById(getterId);
            if (getter == null) return;

            getter.Position = buildingEntity.InteriorColshape.Position;
            getter.GetAccountEntity().characterEntity.Dimension = buildingEntity.DbModel.Id;

            player.SendNativeNotify(null, NotificationNativeType.Building, 1, $"Wyproszono {getter.GetAccountEntity().characterEntity.FormatName} z budynku", "~g~Budynek", "Ta osoba została wyproszona na zewnątrz budynku");
            getter.SendNativeNotify(null, NotificationNativeType.Building, 1, "Wyproszono Cię z budynku", "~g~Budynek", "Zostałeś wyproczony z tego budynku przez osobę uprawnioną");
        }

    }
}
