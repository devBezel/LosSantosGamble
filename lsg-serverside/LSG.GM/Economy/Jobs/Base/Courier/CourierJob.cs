using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using LSG.GM.Economy.Base.Jobs;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Core.Vehicle;
using LSG.GM.Entities.Job;
using System;
using System.Collections.Generic;
using System.Text;


namespace LSG.GM.Economy.Jobs.Base.Courier
{
    public class CourierJob : JobEntity
    {

        public CourierJob(JobEntityModel jobEnityModel) : base(jobEnityModel)
        {
            
        }

        public void Start(CharacterEntity worker)
        {
            worker.CasualJob = this;

            RespawnJobVehicle(worker);

        }

        public void Stop(CharacterEntity worker)
        {
            worker.CasualJob = null;

            DisposeJobVehicle(worker);
        }

        //public List<ItemModel> GetGroupWarehouseOrders()
        //{

        //}
    }
}
