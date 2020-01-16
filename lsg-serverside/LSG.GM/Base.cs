﻿using AltV.Net;
using AltV.Net.Async;
using AltV.Net.ColShape;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Services;
using LSG.BLL.Services.Interfaces;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.BankModels;
using LSG.DAL.Repositories;
using LSG.DAL.Repositories.IRepository;
using LSG.DAL.UnitOfWork;
using LSG.GM.Core.Description;
using LSG.GM.Core.Login;
using LSG.GM.Entities;
using LSG.GM.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LSG.GM
{
    public class Base : AsyncResource
    {
        public override void OnStart()
        {
            Alt.OnPlayerDisconnect += OnPlayerDisconnect;
            Task.Run(async () =>
            {
                AltAsync.OnPlayerConnect += OnPlayerConnect;
                await EntityHelper.LoadServerEntity();
            });

        }

        private async Task OnPlayerConnect(IPlayer player, string reason) => await AltAsync.Do(async () =>
        {
            await EntityHelper.LoadClientEntity(player);
        });

        private void OnPlayerDisconnect(IPlayer player, string reason)
        {
            if (player == null) return;
            player.GetAccountEntity().Dispose();
        }



        public override void OnStop()
        {
            Alt.Log("Serwer wylączony");
        }
    }
}
