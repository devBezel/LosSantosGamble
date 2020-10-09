using AltV.Net;
using AltV.Net.Async;
using AltV.Net.ColShape;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.ServerEvent;
using AltV.Net.EntitySync.SpatialPartitions;
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
using LSG.GM.Core.Streamers;
using LSG.GM.Core.Streamers.ObjectStreamer;
using LSG.GM.Entities;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Dynamic;
using System.Numerics;
using System.Threading.Tasks;

namespace LSG.GM
{
    internal class Base : AsyncResource
    {
        private static int ServerVersion = 69;
        private static string Branch = "beta";
        public static string FormatServerVersion = $"0.0{ServerVersion}_{Branch}";

        public override void OnStart()
        {
            AltEntitySync.Init(1, 100,
               (threadCount, repository) => new ServerEventNetworkLayer(threadCount, repository),
               (entity, threadCount) => (entity.Id % threadCount),
               (entityId, entityType, threadCount) => (entityId % threadCount),
               (threadId) => new LimitedGrid3(50_000, 50_000, 100, 10_000, 10_000, 600),
               new IdProvider());

            // Ładowanie zasobów serwera
            Task.Run(async () =>
            {
                await EntityHelper.LoadServerEntity();
            });


        }


        public override void OnStop()
        {
            // Zapis wszystkich budynków itp
            Alt.Log("Serwer wylączony");
        }
    }
}
