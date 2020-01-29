using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.BuildingModels
{
    public class BuildingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EnumDataType(typeof(BuildingType))]
        public BuildingType BuildingType { get; set; }
        public float EntryFee { get; set; }
        public float ExternalPickupPositionX { get; set; }
        public float ExternalPickupPositionY { get; set; }
        public float ExternalPickupPositionZ { get; set; }
        public float InternalPickupPositionX { get; set; }
        public float InternalPickupPositionY { get; set; }
        public float InternalPickupPositionZ { get; set; }
        public int MaxObjectsCount { get; set; }
        public int CurrentObjectsCount { get; set; }
        public bool HasCCTV { get; set; }
        public bool HasSafe { get; set; }
        public bool SpawnPossible { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }

        public int CreatorId { get; set; }
        public Account Creator { get; set; }

        public int? CharacterId { get; set; }
        public Character Character { get; set; }

        //Grupy pózniej itp

        public virtual IEnumerable<ItemModel> ItemsInBuilding { get; set; }


    }
}
