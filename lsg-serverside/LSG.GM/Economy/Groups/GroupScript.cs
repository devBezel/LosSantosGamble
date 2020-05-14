using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Validators;
using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.GM.Economy.Groups.Base;
using LSG.GM.Economy.Offers;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Entities.Core.Item;
using LSG.GM.Entities.Core.Item.Scripts;
using LSG.GM.Entities.Core.Vehicle;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using LSG.GM.Wrapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LSG.GM.Economy.Groups
{
    public class GroupScript : IScript
    {

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void DisposePlayerDuty(IPlayer player, string reason)
        {
            player.GetData("group:dutyTimer", out Timer dutyTimer);
            if (dutyTimer == null) return;

            AccountEntity accountEntity = player.GetAccountEntity();

            accountEntity.characterEntity.OnDutyGroup.PlayersOnDuty.Remove(accountEntity);
            accountEntity.characterEntity.OnDutyGroup = null;


            dutyTimer.Stop();
            dutyTimer.Dispose();
            Alt.Log($"[GROUP-SCRIPT] Usuwam służbę gracza {accountEntity.characterEntity.FormatName} po wyjściu z serwera");
        }

        [ClientEvent("group:changeWorkerRights")]
        public void ChangeGroupWorkerRights(IPlayer sender, int characterId, int rights, int slot)
        {
            AccountEntity accountEntity = sender.GetAccountEntity();

            GroupSlotValidator slotValidator = new GroupSlotValidator();
            if (!slotValidator.IsValid((byte)slot))
            {
                sender.SendErrorNotify("Ten slot jest niepoprawny!", "Wybierz inny slot, aby wejść na służbę podanej grupy");
                return;
            }

            if (accountEntity.characterEntity.OnDutyGroup == null)
            {
                sender.SendErrorNotify("Musisz być na służbie", "Wejdź na służbę, aby uruchomić panel grupy");
                return;
            }

            if (sender.TryGetGroupByUnsafeSlot((short)slot, out GroupEntity group, out GroupWorkerModel worker))
            {
                if (!group.CanPlayerManageWorkers(worker))
                {
                    sender.SendErrorNotify("Wystąpił bląd", "Nie masz uprawnień do zarządzania członkami");
                    return;
                }

                GroupRights convertedRights = (GroupRights)Enum.Parse(typeof(GroupRights), rights.ToString());
                if ((convertedRights.HasFlag(GroupRights.DepositWithdrawMoney) || convertedRights.HasFlag(GroupRights.Recruitment)) && !group.IsGroupOwner(worker))
                {
                    Alt.Log($"[MEMORY-ALERT] {accountEntity.DbModel.Username} zmienił dane w UI i wysłał emit do serwera z niepoprawnymi danymi");
                    return;
                }

                GroupWorkerModel workerToUpdate = group.DbModel.Workers.FirstOrDefault(c => c.CharacterId == characterId);
                workerToUpdate.Rights = convertedRights;

                group.Save();

                sender.SendSuccessNotify("Wykonano pomyśnie!", $"Zmieniłeś uprawnienia członkowi {workerToUpdate.Character.Name} {workerToUpdate.Character.Surname}");
            }
        }

        [ClientEvent("group:changeWorkerRank")]
        public void ChangeGroupWorkerRank(IPlayer sender, int characterId, int rankToChange, int groupSlot)
        {
            AccountEntity accountEntity = sender.GetAccountEntity();
            GroupSlotValidator slotValidator = new GroupSlotValidator();

            if (!slotValidator.IsValid((byte)groupSlot))
            {
                sender.SendErrorNotify("Ten slot jest niepoprawny!", "Wybierz inny slot, aby wejść na służbę podanej grupy");
                return;
            }

            if (accountEntity.characterEntity.OnDutyGroup == null)
            {
                sender.SendErrorNotify("Musisz być na służbie", "Wejdź na służbę, aby uruchomić panel grupy");
                return;
            }

            if (sender.TryGetGroupByUnsafeSlot((short)groupSlot, out GroupEntity group, out GroupWorkerModel worker))
            {
                if (!group.CanPlayerManageWorkers(worker))
                {
                    sender.SendErrorNotify("Wystąpił bląd", "Nie masz uprawnień do zarządzania członkami");
                    return;
                }

                GroupWorkerModel workerToUpdate = group.DbModel.Workers.SingleOrDefault(c => c.CharacterId == characterId);
                GroupRankModel rankToChangeModel = group.DbModel.Ranks.First(r => r.Id == rankToChange);

                if (worker.GroupRank.Rights < rankToChangeModel.Rights || !group.IsGroupOwner(worker))
                {
                    Alt.Log($"[MEMORY-ALERT] {accountEntity.DbModel.Username} zmienił dane w UI i wysłał emit do serwera z niepoprawnymi danymi");
                    return;
                }

                workerToUpdate.GroupRank = rankToChangeModel;

                group.Save();
                sender.SendChatMessageInfo("Ranga pracownika została zaktualizowana pomyślnie!");
            }
        }


        [Command("gduty")]
        public void EnterDutyGroup(IPlayer player, int slot)
        {
            Timer dutyTimer = new Timer(60000);

            AccountEntity accountEntity = player.GetAccountEntity();

            if (accountEntity.characterEntity.OnDutyGroup != null)
            {
                player.SendSuccessNotify("Zszedłeś ze służby", "Twoja postać zszedła ze służby");
                accountEntity.characterEntity.OnDutyGroup.PlayersOnDuty.Remove(accountEntity);
                accountEntity.characterEntity.OnDutyGroup = null;

                accountEntity.characterEntity.UpdateName(accountEntity.characterEntity.FormatName);
                dutyTimer.Stop();
                dutyTimer.Dispose();

                player.SetData("group:dutyTimer", null);
                player.SetSyncedMetaData("group:dutyWorkerData", null);
                player.SetSyncedMetaData("group:dutyGroupData", null);
            } 
            else
            {
                GroupSlotValidator slotValidator = new GroupSlotValidator();
                if (!slotValidator.IsValid((byte)slot))
                {
                    player.SendErrorNotify("Ten slot jest niepoprawny!", "Wybierz inny slot, aby wejść na służbę podanej grupy");
                    return;
                }

                if (player.TryGetGroupByUnsafeSlot(Convert.ToInt16(slot), out GroupEntity group, out GroupWorkerModel worker))
                {
                    dutyTimer.Start();
                    dutyTimer.Elapsed += (o, args) =>
                    {
                        worker.DutyMinutes += 1;
                        group.Save();
                    };

                    player.SetData("group:dutyTimer", dutyTimer);
                    player.SetSyncedMetaData("group:dutyWorkerData", worker);
                    player.SetSyncedMetaData("group:dutyGroupData", group.DbModel);

                    accountEntity.characterEntity.UpdateName($"~w~(~b~{group.DbModel.Tag}~w~) {accountEntity.characterEntity.FormatName}");
                    accountEntity.characterEntity.OnDutyGroup = group;
                    accountEntity.characterEntity.OnDutyGroup.PlayersOnDuty.Add(accountEntity);
                    player.SendSuccessNotify("Wszedłeś na służbę", $"Wszedłeś na służbę {group.DbModel.Name}");

                } else
                {
                    player.SendErrorNotify("Nie posiadasz grupy o tym slocie", "Twoja postać nie posiada grupy w tym slocie");
                }
            }
        }



        [Command("g")]
        public void OpenGroupPanel(IPlayer player, int slot)
        {
            AccountEntity accountEntity = player.GetAccountEntity();

            GroupSlotValidator slotValidator = new GroupSlotValidator();
            if (!slotValidator.IsValid((byte)slot))
            {
                player.SendErrorNotify("Ten slot jest niepoprawny!", "Wybierz inny slot, aby wejść na służbę podanej grupy");
                return;
            }

            if (accountEntity.characterEntity.OnDutyGroup == null)
            {
                player.SendErrorNotify("Musisz być na służbie", "Wejdź na służbę, aby uruchomić panel grupy");
                return;
            }
            // Naprawić wyświetlanie użytkowników online
            if (player.TryGetGroupByUnsafeSlot(Convert.ToInt16(slot), out GroupEntity group, out GroupWorkerModel worker))
            {
                player.Emit("group-general:openGroupPanel",
                    group.DbModel,
                    group.GetWorkers().Where(c => c.Character.Online).ToList(),
                    group.DbModel.Ranks,
                    group.DbModel.Vehicles,
                    worker,
                    slot);
            }
        }

        [Command("gzapros")]
        public void InvitePlayerToGroup(IPlayer sender, int groupSlot, int getterId)
        {
            IPlayer getter = PlayerExtenstion.GetPlayerById(getterId);
            if (getter == null)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Nie ma gracza o podanym ID na serwerze");
                return;
            }

            GroupSlotValidator slotValidator = new GroupSlotValidator();
            if (!slotValidator.IsValid((byte)groupSlot))
            {
                sender.SendErrorNotify("Ten slot jest niepoprawny!", "Wybierz inny slot, aby zaprosić gracza do grupy");
                return;
            }

            if (sender.TryGetGroupByUnsafeSlot((short)groupSlot, out GroupEntity group, out GroupWorkerModel groupWorker))
            {
                if (!group.CanPlayerManageWorkers(groupWorker))
                {
                    sender.SendErrorNotify("Nie masz uprawnień", "Nie masz uprawnień, aby zaprosić gracza do grupy");
                    return;
                }

                if (group.ContainsWorker(getter.GetAccountEntity()))
                {
                    sender.SendErrorNotify("Wystąpił bląd", "Gracz znajduje się już w grupie");
                    return;
                }

                group.AddWorker(getter.GetAccountEntity());
                getter.SendSuccessNotify("Dodano Cię do grupy", $"Zostałeś dodany do grupy {group.DbModel.Tag}");
            }


        }

        #region Search Player
        [Command("przeszukaj")]
        public async Task SearchPlayer(IPlayer sender, int getterId)
        {
            CharacterEntity characterEntity = sender.GetAccountEntity().characterEntity;
            CharacterEntity getterCharacterEntity = PlayerExtenstion.GetPlayerById(getterId).GetAccountEntity().characterEntity;

            Alt.Log("Przeszło characterEntity");

            if (getterCharacterEntity == null)
                return;

            if (!Calculation.IsPlayerInRange(sender, getterCharacterEntity.AccountEntity.Player, 2))
            {
                sender.SendChatMessageError("Gracz którego chcesz zakuć jest zbyt daleko");
                return;
            }


            Alt.Log("Przeszło getterCharacterEntity");
            if (characterEntity.OnDutyGroup is Police group)
            {
                Alt.Log("Wykonalo sie z policja");
                Alt.Log($"{group.CanPlayerDoPolice(characterEntity.AccountEntity)}");
                if (!group.CanPlayerDoPolice(characterEntity.AccountEntity))
                {
                    sender.SendChatMessageError("Nie posiadasz uprawnień, aby to zrobić.");
                    return;
                }
                getterCharacterEntity.AccountEntity.Player.SendChatMessageInfo($"Jesteś przeszukiwany przez {characterEntity.DbModel.Name} {characterEntity.DbModel.Surname}");

                await sender.EmitAsync("group:searchPlayer", getterCharacterEntity.DbModel.Items);
            }

        }

        [ClientEvent("group:confiscatePlayerItem")]
        public void ConfiscatePlayerItem(IPlayer robber, int itemID, int getterID)
        {
            CharacterEntity robbedCharacterEntity = PlayerExtenstion.GetPlayerByCharacterId(getterID);
            if (robbedCharacterEntity == null)
                return;

            ItemModel itemToConfiskate = robbedCharacterEntity.DbModel.Items.First(x => x.Id == itemID);
            CharacterEntity robberCharacterEntity = robber.GetAccountEntity().characterEntity;

            ItemEntity itemEntity = null;

            if (robbedCharacterEntity.ItemsInUse.FirstOrDefault(x => x.Id == itemID) == null)
            {
                itemEntity = InventoryScript.ItemFactory.Create(itemToConfiskate);
            }
            else
            {
                itemEntity = robbedCharacterEntity.ItemsInUse.First(x => x.Id == itemID);
            }



            itemEntity.Confiscate(robberCharacterEntity, robbedCharacterEntity);
            robbedCharacterEntity.AccountEntity.Player.SendChatMessageInfo($"{robberCharacterEntity.DbModel.Name} {robberCharacterEntity.DbModel.Surname} zabrał Ci {itemToConfiskate.Name}");

        }

        #endregion

        #region Cuff Player
        [Command("zakuj")]
        public async Task CuffPlayer(IPlayer sender, int getterId)
        {
            GroupEntity group = sender.GetAccountEntity().characterEntity.OnDutyGroup;
            if (group == null) return;

            if (group.DbModel.GroupType != GroupType.Police || !((Police)group).CanPlayerDoPolice(sender.GetAccountEntity()))
            {
                sender.SendChatMessageError("Nie masz uprawnień do używania kajdanek");
                return;
            }

            IPlayer getter = PlayerExtenstion.GetPlayerById(getterId);
            if (getter == null)
            {
                sender.SendChatMessageError("Gracza o podanym ID nie ma w grze");
                return;
            }

            if (!Calculation.IsPlayerInRange(sender, getter, 2))
            {
                sender.SendChatMessageError("Gracz którego chcesz zakuć jest zbyt daleko");
                return;
            }

            CharacterEntity getterCharacterEntity = getter.GetAccountEntity().characterEntity;

            if (getterCharacterEntity.IsHandcuffed)
            {
                sender.PlayAnimation("arrest", "arrest_fallback_high_cop", 3000);

                //getter.SetEnableHandcuffs(false);
                //getter.ClearTasks();
                //getter.DisablePlayerFiring(false);
                //getter.SetCanPlayGestureAnims(true);
                //getter.FreezePosition(false);

                getterCharacterEntity.IsHandcuffed = false;
                await getter.EmitAsync("group:uncuffPlayer");
            }
            else
            {
                sender.PlayAnimation("arrest", "arrest_fallback_high_cop", 3000);
                await Task.Delay(3000);

                getter.PlayAnimation("mp_arresting", "idle", -1, 49);
                //getter.SetEnableHandcuffs(true);
                //getter.DisablePlayerFiring(true);
                //getter.SetCanPlayGestureAnims(false);
                //getter.FreezePosition(true);
                getterCharacterEntity.IsHandcuffed = true;
                await getter.EmitAsync("group:cuffPlayer");

            }

        }

        #endregion

        #region Drag Player

        [Command("prowadz")]
        public async Task DragPlayer(IPlayer sender, int getterId)
        {
            GroupEntity group = sender.GetAccountEntity().characterEntity.OnDutyGroup;
            if (group == null) return;

            if (group.DbModel.GroupType != GroupType.Police || !((Police)group).CanPlayerDoPolice(sender.GetAccountEntity()))
            {
                sender.SendChatMessageError("Nie masz uprawnień do używania kajdanek");
                return;
            }

            IPlayer getter = PlayerExtenstion.GetPlayerById(getterId);
            if (getter == null)
            {
                sender.SendChatMessageError("Gracza o podanym ID nie ma w grze");
                return;
            }

            if (!Calculation.IsPlayerInRange(sender, getter, 2))
            {
                sender.SendChatMessageError("Gracz którego chcesz zakuć jest zbyt daleko");
                return;
            }

            CharacterEntity getterCharacterEntity = getter.GetAccountEntity().characterEntity;

            if (!getterCharacterEntity.IsHandcuffed)
            {
                sender.SendChatMessageError("Gracz musi być zakuty w kajdanki jeśli chcesz go przenieść");
                return;
            }


            if (getterCharacterEntity.IsDragged)
            {
                getterCharacterEntity.IsDragged = false;
                await sender.EmitAsync("group:undragPlayer");
            }
            else
            {
                getterCharacterEntity.IsDragged = true;
                await sender.EmitAsync("group:dragPlayer", getter.Id);
            }
        }
        #endregion

        #region Resuscitation Player
        // TODO: Zrobić to jako oferte od grupy
        [Command("reanimuj")]
        public void ResuscitationPlayer(IPlayer sender, int getterId)
        {
            CharacterEntity senderCharacterEntity = sender.GetAccountEntity().characterEntity;
            CharacterEntity getterCharacterEntity = PlayerExtenstion.GetPlayerById(getterId).GetAccountEntity().characterEntity;

            Alt.Log("Przeszło characterEntity");

            if (getterCharacterEntity == null)
                return;

            if (!Calculation.IsPlayerInRange(sender, getterCharacterEntity.AccountEntity.Player, 2))
            {
                sender.SendChatMessageError("Tego gracza nie ma w pobliżu");
                return;
            }

            if (!getterCharacterEntity.HasBw)
            {
                sender.SendChatMessageError("Ten gracz żyje, nie możesz go reanimować");
                return;
            }

            if (senderCharacterEntity.OnDutyGroup is Paramedic group)
            {
                if (group.CanPlayerResuscitation(senderCharacterEntity.AccountEntity))
                {
                    senderCharacterEntity.AccountEntity.Player.SendChatMessageError("Nie masz uprawnień do użycia tej komendy");
                    return;
                }

                OfferScript.OfferPlayer(sender, "Pomoc medyczna", getterId, OfferType.ResuscitationPlayer, 0, 120);
            }


        }
        #endregion

        [ClientEvent("vehicle-script:removeUpgrade")]
        public void RemoveVehicleUpgrade(IPlayer sender, int itemID)
        {
            CharacterEntity senderCharacterEntity = sender.GetAccountEntity().characterEntity;
            if(senderCharacterEntity.OnDutyGroup is Mechanic group)
            {
                if(!group.CanPlayerTuningVehicle(senderCharacterEntity.AccountEntity))
                {
                    sender.SendChatMessageError("Nie masz uprawnień, aby demontować części w samochodach");
                    return;
                }

                VehicleEntity vehicleToRemoveUpgrade = sender.Vehicle.GetVehicleEntity();
                if (vehicleToRemoveUpgrade == null)
                    return;

                CharacterEntity ownerVehicle = PlayerExtenstion.GetPlayerByCharacterId(vehicleToRemoveUpgrade.DbModel.OwnerId);
                if (ownerVehicle == null || !ownerVehicle.DbModel.Online)
                {
                    sender.SendChatMessageError("Ten gracz musi być w grze, abyś mógł zamontować część do jego pojazdu");
                    return;
                }

                OfferScript.OfferPlayer(sender, "Demontaż części", ownerVehicle.AccountEntity.ServerID, OfferType.TuningVehicle, itemID, 100);
            }
            else
            {
                sender.SendChatMessageError("Nie masz uprawnień do demontowania części w samochodzie");
            }
        }
    }
}
