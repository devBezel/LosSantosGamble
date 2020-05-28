using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.GM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Job
{
    public class JobCenterEntity
    {
        public JobCenterModel JobCenterModel { get; set; }
        public IColShape Colshape { get; set; }
        public MarkerModel Marker { get; set; }


        public JobCenterEntity(JobCenterModel jobCenterModel)
        {
            JobCenterModel = jobCenterModel;
        }

        public void Spawn()
        {
            Colshape = Alt.CreateColShapeCylinder(new Position(JobCenterModel.Position.X, JobCenterModel.Position.Y, JobCenterModel.Position.Z - 0.9f), 1f, 2f);
            Marker = new MarkerModel()
            {
                Type = 1,
                Dimension = 0,
                PosX = JobCenterModel.Position.X,
                PosY = JobCenterModel.Position.Y,
                PosZ = JobCenterModel.Position.Z - 0.9f,
                DirX = 0,
                DirY = 0,
                DirZ = 0,
                RotX = 0,
                RotY = 0,
                RotZ = 0,
                ScaleX = 1f,
                ScaleY = 1f,
                ScaleZ = 1f,
                Red = 0,
                Green = 153,
                Blue = 0,
                Alpha = 100,
                BobUpAndDown = false,
                FaceCamera = false,
                P19 = 2,
                Rotate = false,
                TextureDict = null,
                TextureName = null,
                DrawOnEnts = false,
                UniqueID = $"JOB_CENTER_MARKER{JobCenterModel.Id}"
            };
            // TODO: Stworzenie PEDa
            Colshape.SetData("job-center:data", this);
            EntityHelper.Add(this);
        }


    }

    public class JobCenterModel
    {
        public int Id { get; set; }
        public Position Position { get; set; }
        public List<JobEntityModel> Jobs { get; set; }
    }
}
