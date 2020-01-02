using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Resources.Chat.Api;
using LSG.BLL.Services;
using LSG.BLL.Services.Interfaces;
using LSG.DAL.Database;
using LSG.DAL.Repositories;
using LSG.DAL.Repositories.IRepository;
using LSG.DAL.UnitOfWork;
using LSG.GM.Core.Description;
using LSG.GM.Core.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LSG.GM
{
    public class Base : AsyncResource
    {
        public override void OnStart()
        {            
            new LoginScript();
            new DescriptionScript();
        }

        public override void OnStop()
        {
            Alt.Log("Serwer wylączony");
        }
    }
}
