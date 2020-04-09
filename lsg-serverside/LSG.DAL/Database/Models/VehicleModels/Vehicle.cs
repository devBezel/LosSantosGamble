using AltV.Net;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Database.Models.ItemModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LSG.DAL.Database.Models.VehicleModels
{
    public class Vehicle : IWritable
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int? OwnerId { get; set; }
        public Character Owner { get; set; }

        public int? GroupId { get; set; }
        public GroupModel Group { get; set; }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float RotRoll { get; set; }
        public float RotPitch { get; set; }
        public float RotYaw { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public bool State { get; set; }
        public int Health { get; set; }
        //public int Dimension { get; set; }

        public List<ItemModel> ItemsInVehicle { get; set; }


        public void OnWrite(IMValueWriter writer)
        {
            writer.BeginObject();

            writer.Name("id");
            writer.Value(Id);

            writer.Name("model");
            writer.Value(Model);

            writer.Name("ownerId");
            if(OwnerId.HasValue)
            {
                writer.Value(OwnerId.Value);
            }

            writer.Name("groupId");
            if(GroupId.HasValue)
            {
                writer.Value(GroupId.Value);
            }

            writer.Name("posX");
            writer.Value(PosX);

            writer.Name("posY");
            writer.Value(PosY);

            writer.Name("posZ");
            writer.Value(PosZ);

            writer.Name("rotRoll");
            writer.Value(RotRoll);

            writer.Name("rotPitch");
            writer.Value(RotPitch);

            writer.Name("rotYaw");
            writer.Value(RotYaw);

            writer.Name("r");
            writer.Value(R);

            writer.Name("g");
            writer.Value(G);

            writer.Name("b");
            writer.Value(B);

            writer.Name("state");
            writer.Value(State);

            writer.Name("health");
            writer.Value(Health);

            writer.EndObject();

        }

    }
}
