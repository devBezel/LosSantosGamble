using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Validators;
using LSG.DAL.Database.Models.GroupModels;
using LSG.GM.Economy.Groups.Base;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Group;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace LSG.GM.Economy.Groups
{
    public class GroupGeneralScript : IScript
    {

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

            if(player.TryGetGroupByUnsafeSlot(Convert.ToInt16(slot), out GroupEntity group, out GroupWorkerModel worker))
            {
                player.Emit("group-general:openGroupPanel", group.DbModel, group.DbModel.Workers, group.DbModel.Vehicles, worker);
            }
        }
    }
}
