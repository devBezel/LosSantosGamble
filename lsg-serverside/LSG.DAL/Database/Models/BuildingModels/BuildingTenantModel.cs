using AltV.Net;
using LSG.DAL.Database.Models.CharacterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.BuildingModels
{
    public class BuildingTenantModel : IWritable
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


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("buildingId");
            writer.Value(BuildingId);

            writer.Name("characterId");
            writer.Value(CharacterId);

            writer.Name("tenantAdded");
            writer.Value(TenantAdded.ToString());

            writer.Name("canEditBuilding");
            writer.Value(CanEditBuilding);

            writer.Name("canWithdrawDeposit");
            writer.Value(CanWithdrawDeposit);

            writer.Name("canManagmentTenants");
            writer.Value(CanManagmentTenants);

            writer.Name("canManagmentMagazine");
            writer.Value(CanManagmentMagazine);

            writer.Name("canRespawnInBuilding");
            writer.Value(CanRespawnInBuilding);

            writer.Name("canLockDoor");
            writer.Value(CanLockDoor);

            writer.Name("canManagmentGuests");
            writer.Value(CanManagmentGuests);

            writer.EndObject();

        }

    }
}
