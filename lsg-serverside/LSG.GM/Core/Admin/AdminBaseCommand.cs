using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Entities.Admin;
using LSG.GM.Entities.Core;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AltV.Net.Data;
using System.Linq;
using LSG.GM.Economy.Money;
using LSG.GM.Utilities;

namespace LSG.GM.Core.Admin
{
    public class AdminBaseCommand : IScript 
    {
        [Command("aduty")]
        public void StartAdminDutyCMD(IPlayer player)
        {
            if (!player.HasRank((int)EAdmin.Supporter))
                return;

            player.ChangeAdminDutyState();
        }

        [Command("tveh")]
        public async Task SpawnTemporaryVehicleCMD(IPlayer sender, VehicleModel model) => await AltAsync.Do(async () =>
        {
            if (!sender.HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.OnAdminDuty())
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }
                      
            if (sender.GetData("admin:vehicle", out IVehicle adminVehicle))
                await adminVehicle.RemoveAsync();

            Task<IVehicle> vehicle = AltAsync.CreateVehicle(model, new Position(sender.Position.X + 5, sender.Position.Y, sender.Position.Z), sender.Rotation);
            IVehicle veh = await vehicle.ConfigureAwait(false);

            sender.SetData("admin:vehicle", veh);
            await veh.SetNumberplateTextAsync("ADMIN: " + sender.GetAccountEntity().Id);
            sender.SendSuccessNotify(null, $"Zrespiłeś {model}");
        });

        [Command("tp")]
        public async Task TeleportToPlayerCMD(IPlayer sender, int id) => await AltAsync.Do(() =>
        {
            if (!sender.HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.OnAdminDuty())
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            IPlayer getter = PlayerExtenstion.GetPlayerById(id);

            sender.Position = getter.Position;

            sender.SendSuccessNotify(null, $"Przeteleportowałeś się do ID: {id}");
        });

        [Command("addmoney")]
        public async Task AddCharacterMoneyCMD(IPlayer sender, int amount, int id) => await AltAsync.Do(() =>
        {
            if (!sender.HasRank((int)EAdmin.Administrator))
                return;

            if (!sender.OnAdminDuty())
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            IPlayer getter = PlayerExtenstion.GetPlayerById(id);

            getter.AddMoney(amount);

            sender.SendSuccessNotify(null, $"Nadałeś graczowi ID: {id} {amount}$");
            getter.SendSuccessNotify(null, $"Otrzymałeś od administratora {sender.GetAccountEntity().Username} {amount}$");
        });

        [Command("testmoney")]
        public void TestMoneyCMD(IPlayer player)
        {
            player.SendSuccessNotify(null, $"Twoja ilość gotówki: {player.GetCharacterEntity().Money}");

            player.SendChatMessage($"{Singleton.GetDatabaseInstance().Accounts.SingleOrDefault(x => x.Id == player.GetAccountEntity().Id).PasswordHash}");
        }
    }
}
