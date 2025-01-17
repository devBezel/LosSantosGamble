﻿using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using AltV.Net.Resources.Chat.Api;
//using LSG.GM.Entities.Admin;
using LSG.GM.Entities.Core;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AltV.Net.Data;
using System.Linq;
//using LSG.GM.Economy.Money;
using LSG.GM.Utilities;
using LSG.DAL.Database.Models.BankModels;
using LSG.GM.Entities.Common.Atm;
using AtmModel = LSG.DAL.Database.Models.BankModels.Atm;


namespace LSG.GM.Core.Admin
{
    public class AdminBaseCommand : IScript 
    {
        [Command("aduty")]
        public void StartAdminDutyCMD(IPlayer player)
        {
            if (!player.GetAccountEntity().HasRank((int)EAdmin.Supporter))
                return;

            player.GetAccountEntity().ChangeAdminDutyState();
        }

        [Command("tveh")]
        public async Task SpawnTemporaryVehicleCMD(IPlayer sender, VehicleModel model) => await AltAsync.Do(async () =>
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }
                      
            if (sender.GetData("admin:vehicle", out IVehicle adminVehicle))
                await adminVehicle.RemoveAsync();

            Task<IVehicle> vehicle = AltAsync.CreateVehicle(model, new Position(sender.Position.X + 5, sender.Position.Y, sender.Position.Z), sender.Rotation);
            IVehicle veh = await vehicle.ConfigureAwait(false);

            sender.SetData("admin:vehicle", veh);
            await veh.SetNumberplateTextAsync("ADMIN: " + sender.GetAccountEntity().DbModel.Id);
            sender.SendSuccessNotify(null, $"Zrespiłeś {model}");
        });

        [Command("tp")]
        public async Task TeleportToPlayerCMD(IPlayer sender, int id) => await AltAsync.Do(() =>
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            IPlayer getter = PlayerExtenstion.GetPlayerById(id);
            if (getter == null)
            {
                sender.SendErrorNotify(null, $"Gracz o ID {id} nie jest w grze");
                return;
            }

            sender.Position = getter.Position;

            sender.SendSuccessNotify(null, $"Przeteleportowałeś się do ID: {id}");
        });

        [Command("addmoney")]
        public async Task AddCharacterMoneyCMD(IPlayer sender, int id, int amount) => await AltAsync.Do(() =>
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Administrator))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            IPlayer getter = PlayerExtenstion.GetPlayerById(id);
            if (getter == null)
            {
                sender.SendErrorNotify(null, $"Gracz o ID {id} nie jest w grze");
            }
            getter.GetAccountEntity().characterEntity.AddMoney(amount, false);
            //getter.AddMoney(amount);

            sender.SendSuccessNotify(null, $"Nadałeś graczowi ID: {id} {amount}$");

            if (sender.GetAccountEntity().DbModel.Id == getter.GetAccountEntity().DbModel.Id) return;
            getter.SendSuccessNotify(null, $"Otrzymałeś od administratora {sender.GetAccountEntity().DbModel.Username} {amount}$");
        });


        [Command("tweapon")]
        public async Task CreateTemporaryWeaponCMD(IPlayer sender, WeaponModel model) => await AltAsync.Do(() =>
        {
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Supporter))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            sender.GiveWeaponAsync((uint)model, 200, false);
        });
    }
}
