using AltV.Net;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LSG.DAL.Database.Models.BuildingModels
{
    public class BuildingModel : IWritable
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

        public bool OnSale { get; set; }
        public int SaleCost { get; set; }

        public int CreatorId { get; set; }
        public Account Creator { get; set; }

        public int? CharacterId { get; set; }
        public Character Character { get; set; }

        //Grupy pózniej itp

        public virtual IEnumerable<ItemModel> ItemsInBuilding { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("name");
            writer.Value(Name);

            writer.Name("buildingType");
            writer.Value((int)BuildingType);

            writer.Name("entryFee");
            writer.Value(EntryFee);

            writer.Name("externalPickupPositionX");
            writer.Value(ExternalPickupPositionX);

            writer.Name("externalPickupPositionY");
            writer.Value(ExternalPickupPositionY);

            writer.Name("externalPickupPositionZ");
            writer.Value(ExternalPickupPositionZ);

            writer.Name("internalPickupPositionX");
            writer.Value(InternalPickupPositionX);

            writer.Name("internalPickupPositionY");
            writer.Value(InternalPickupPositionY);

            writer.Name("internalPickupPositionZ");
            writer.Value(InternalPickupPositionZ);

            writer.Name("maxObjectsCount");
            writer.Value(MaxObjectsCount);

            writer.Name("currentObjectsCount");
            writer.Value(CurrentObjectsCount);

            writer.Name("hasCCTV");
            writer.Value(HasCCTV);

            writer.Name("hasSafe");
            writer.Value(HasSafe);

            writer.Name("spawnPossible");
            writer.Value(SpawnPossible);

            writer.Name("description");
            writer.Value(Description);

            writer.Name("createdTime");
            writer.Value(CreatedTime.ToString());

            writer.Name("onSale");
            writer.Value(OnSale);

            writer.Name("saleCost");
            writer.Value(SaleCost);

            if(CharacterId.HasValue)
            {
                writer.Name("characterId");
                writer.Value(CharacterId.Value);
            }

            writer.Name("itemsInBuilding");
            writer.Value(JsonConvert.SerializeObject(ItemsInBuilding));


            writer.EndObject();

        }
    }
}
