using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Validators;
using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.GM.Economy.Groups.Base;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Entities.Core.Item;
using LSG.GM.Entities.Core.Item.Scripts;
using LSG.GM.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LSG.GM.Economy.Groups
{
    public class GroupGeneralScript : IScript
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

            if(sender.TryGetGroupByUnsafeSlot((short)slot, out GroupEntity group, out GroupWorkerModel worker))
            {
                Alt.Log($" group.CanPlayerManageWorkers {group.CanPlayerManageWorkers(worker)}");
                if(!group.CanPlayerManageWorkers(worker))
                {
                    sender.SendErrorNotify("Wystąpił bląd", "Nie masz uprawnień do zarządzania członkami");
                    return;
                }
                Alt.Log("Wczytalo sie grupy");

                GroupWorkerModel workerToUpdate = group.DbModel.Workers.FirstOrDefault(c => c.CharacterId == characterId);
                workerToUpdate.Rights = (GroupRights)rights;

                group.Save();

                sender.SendSuccessNotify("Wykonano pomyśnie!", $"Zmieniłeś uprawnienia członkowi {workerToUpdate.Character.Name} {workerToUpdate.Character.Surname}");
            }
        }


        [Command("gduty")]
        public void EnterDutyGroup(IPlayer player, int slot)
        {
            Timer dutyTimer = new Timer(60000);

            AccountEntity accountEntity = player.GetAccountEntity();
            if(accountEntity.characterEntity.OnDutyGroup != null)
            {
                player.SendSuccessNotify("Zszedłeś ze służby", "Twoja postać zszedła ze służby");
                accountEntity.characterEntity.OnDutyGroup.PlayersOnDuty.Remove(accountEntity);
                accountEntity.characterEntity.OnDutyGroup = null;

                accountEntity.characterEntity.UpdateName(accountEntity.characterEntity.FormatName);
                dutyTimer.Stop();
                dutyTimer.Dispose();

                player.SetData("group:dutyTimer", null);
            } else
            {
                GroupSlotValidator slotValidator = new GroupSlotValidator();
                if(!slotValidator.IsValid((byte)slot))
                {
                    player.SendErrorNotify("Ten slot jest niepoprawny!", "Wybierz inny slot, aby wejść na służbę podanej grupy");
                    return; 
                }

                if(player.TryGetGroupByUnsafeSlot(Convert.ToInt16(slot), out GroupEntity group, out GroupWorkerModel worker))
                {
                    dutyTimer.Start();
                    dutyTimer.Elapsed += (o, args) =>
                    {
                        worker.DutyMinutes += 1;
                        group.Save();
                    };

                    player.SetData("group:dutyTimer", dutyTimer);

                    accountEntity.characterEntity.UpdateName($"(~b~{group.DbModel.Tag}~w~) {accountEntity.characterEntity.FormatName}");
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
            if(!slotValidator.IsValid((byte)slot))
            {
                player.SendErrorNotify("Ten slot jest niepoprawny!", "Wybierz inny slot, aby wejść na służbę podanej grupy");
                return;
            }

            if(accountEntity.characterEntity.OnDutyGroup == null)
            {
                player.SendErrorNotify("Musisz być na służbie", "Wejdź na służbę, aby uruchomić panel grupy");
                return;
            }
            // Naprawić wyświetlanie użytkowników online
            if(player.TryGetGroupByUnsafeSlot(Convert.ToInt16(slot), out GroupEntity group, out GroupWorkerModel worker))
            {
                player.Emit("group-general:openGroupPanel", 
                    group.DbModel, 
                    group.GetWorkers().Where(c => c.Character.Online).ToList(),
                    group.DbModel.Vehicles, 
                    worker, 
                    slot);
            }
        }

        [Command("gzapros")]
        public void InvitePlayerToGroup(IPlayer sender, int groupSlot, int getterId)
        {
            IPlayer getter = PlayerExtenstion.GetPlayerById(getterId);
            if(getter == null)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Nie ma gracza o podanym ID na serwerze");
                return;
            }

            GroupSlotValidator slotValidator = new GroupSlotValidator();
            if(!slotValidator.IsValid((byte)groupSlot))
            {
                sender.SendErrorNotify("Ten slot jest niepoprawny!", "Wybierz inny slot, aby zaprosić gracza do grupy");
                return;
            }

            if(sender.TryGetGroupByUnsafeSlot((short)groupSlot, out GroupEntity group, out GroupWorkerModel groupWorker))
            {
                if(!group.CanPlayerManageWorkers(groupWorker))
                {
                    sender.SendErrorNotify("Nie masz uprawnień", "Nie masz uprawnień, aby zaprosić gracza do grupy");
                    return;
                }

                if(group.ContainsWorker(getter.GetAccountEntity()))
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
        public async Task SearchPlayer(IPlayer player, int getterId)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            CharacterEntity getterCharacterEntity = PlayerExtenstion.GetPlayerById(getterId).GetAccountEntity().characterEntity;

            Alt.Log("Przeszło characterEntity");

            if (getterCharacterEntity == null)
                return;

            Alt.Log("Przeszło getterCharacterEntity");
            if (characterEntity.OnDutyGroup is Police group)
            {
                Alt.Log("Wykonalo sie z policja");
                Alt.Log($"{group.CanPlayerDoPolice(characterEntity.AccountEntity)}");
                if (!group.CanPlayerDoPolice(characterEntity.AccountEntity))
                {
                    player.SendChatMessageError("Nie posiadasz uprawnień, aby to zrobić.");
                    return;
                }
                getterCharacterEntity.AccountEntity.Player.SendChatMessageInfo($"Jesteś przeszukiwany przez {characterEntity.DbModel.Name} {characterEntity.DbModel.Surname}");

                await player.EmitAsync("group:searchPlayer", getterCharacterEntity.DbModel.Items);
            }

        }

        [ClientEvent("group:confiscatePlayerItem")]
        public void ConfiscatePlayerItem(IPlayer robber, string itemJson)
        {
            ItemModel itemToConfiskate = JsonConvert.DeserializeObject<ItemModel>(itemJson);
            CharacterEntity robberCharacterEntity = robber.GetAccountEntity().characterEntity;

            ItemEntity itemEntity = InventoryScript.ItemFactory.Create(itemToConfiskate);
            CharacterEntity robbedCharacterEntity = PlayerExtenstion.GetPlayerByCharacterId(itemToConfiskate.CharacterId);
            if (robbedCharacterEntity == null)
                return;

            itemEntity.Confiscate(robberCharacterEntity, robbedCharacterEntity);
            robbedCharacterEntity.AccountEntity.Player.SendChatMessageInfo($"{robberCharacterEntity.DbModel.Name} {robberCharacterEntity.DbModel.Surname} zabrał Ci {itemToConfiskate.Name}");
            
        }

        #endregion
    }
}
