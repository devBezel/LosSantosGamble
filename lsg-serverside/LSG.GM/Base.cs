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


            CreateObjects();

            //Alt.OnPlayerDisconnect += OnPlayerDisconnect;
            Task.Run(async () =>
            {
                await EntityHelper.LoadServerEntity();
            });

        }

        private void CreateObjects()
        {
            // Create some objects
            ObjectStreamer.CreateDynamicObject("port_xr_lifeboat", new Vector3(-859.655f, -803.499f, 25.566f), new Vector3(0, 0, 0), 0, null, true, null, null, null, null, true, 400);
            ObjectStreamer.CreateDynamicObject("bkr_prop_biker_bowlpin_stand", new Vector3(-959.655f, -903.499f, 25.566f), new Vector3(0, 0, 0), 0, null, true, null, null, null, null, true, 400);
            ObjectStreamer.CreateDynamicObject("bkr_prop_biker_tube_crn", new Vector3(-909.655f, -953.499f, 25.566f), new Vector3(0, 0, 0), 0, null, true, null, null, null, null, true, 400);
        }


        public override void OnStop()
        {
            // Zapis wszystkich budynków itp
            Alt.Log("Serwer wylączony");
        }
    }
}
