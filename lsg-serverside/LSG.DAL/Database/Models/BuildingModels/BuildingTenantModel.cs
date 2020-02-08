using LSG.DAL.Database.Models.CharacterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.BuildingModels
{
    public class BuildingTenantModel
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public BuildingModel Building { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }



        public DateTime TenantAdded { get; set; } = DateTime.Now;

        public bool CanEditBuilding { get; set; }
        public bool CanWithdrawDeposit { get; set; }
        public bool CanManagmentTenants { get; set; }
        public bool CanManagmentMagazine { get; set; }
        public bool CanRespawnInBuilding { get; set; }
        public bool CanLockDoor { get; set; }
        public bool CanManagmentGuests { get; set; }

    }
}
