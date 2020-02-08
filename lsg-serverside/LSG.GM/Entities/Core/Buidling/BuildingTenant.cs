using AltV.Net.Elements.Entities;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LSG.GM.Entities.Core.Buidling
{
    public class BuildingTenant
    {
        private BuildingTenantModel buildingTenant;

        public BuildingTenant(BuildingEntity buildingEntity, CharacterEntity characterEntity)
        {
            buildingTenant = buildingEntity.DbModel.BuildingTenants.SingleOrDefault(x => x.CharacterId == characterEntity.DbModel.Id);
        }

        public bool CharacterIsTenant()
        {
            return buildingTenant != null ? true : false;
        }

        public bool CanEditBuilding()
        {
            return buildingTenant.CanEditBuilding;
        }

        public bool CanWithdrawDeposit()
        {
            return buildingTenant.CanWithdrawDeposit;
        }
    }
}
