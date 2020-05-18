using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Core;
using LSG.GM.Entities.Job;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using LSG.GM.Helpers;
using LSG.GM.Helpers.Models;
using LSG.GM.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Economy.Jobs.Base.Junker
{
    public class JunkerJob : JobEntity
    {
        List<TrashPointModel> TrashPoints = new List<TrashPointModel>()
        {
            new TrashPointModel() { Id = 0, TrashPosition = new Position(395.684f, -719.367f, 29.2799f) },
            new TrashPointModel() { Id = 1, TrashPosition = new Position(412.655f, -796.879f,  29.2799f) },
        };

        public JunkerJob(JobEntityModel jobEntityModel) : base(jobEntityModel)
        {
            foreach (TrashPointModel trash in TrashPoints)
            {
                IColShape trashColshape = Alt.CreateColShapeCylinder(new Position(trash.TrashPosition.X, trash.TrashPosition.Y, trash.TrashPosition.Z - 0.9f), 2f, 2f);
                trashColshape.SetData("trash:data", trash);
            }
        }

        public void GetTrashPoint(CharacterEntity worker)
        {
            if (worker.DbModel.JobEarned >= worker.CasualJob.JobEntityModel.MaxSalary)
            {
                worker.AccountEntity.Player.SendChatMessageInfo("Zarobiłeś już maksymalną ilość pieniędzy. Przyjedź jutro");
                return;
            }


            Random random = new Random();

            int index = random.Next(TrashPoints.Count);
            worker.CurrentTrashPoint = TrashPoints[index];

            worker.AccountEntity.Player.CreateDrawText(new DrawTextModel()
            {
                Text = "Kliknij ~g~E ~w~ aby opróżnić kosz",
                X = worker.CurrentTrashPoint.TrashPosition.X,
                Y = worker.CurrentTrashPoint.TrashPosition.Y,
                Z = worker.CurrentTrashPoint.TrashPosition.Z,
                Dimension = 0,
                UniqueID = $"JUNKER_TRASH_POINT_DRAW_TEXT{worker.CurrentTrashPoint.Id}"
            });

            Task.Run(async () =>
            {
                await worker.AccountEntity.Player.CreateBlip(new BlipModel()
                {
                    Name = $"Śmietnik",
                    Blip = 1,
                    Color = 73,
                    PosX = worker.CurrentTrashPoint.TrashPosition.X,
                    PosY = worker.CurrentTrashPoint.TrashPosition.Y,
                    PosZ = worker.CurrentTrashPoint.TrashPosition.Y,
                    ShortRange = false,
                    Size = EBlipSize.Medium,
                    UniqueID = $"JUNKER_TRASH_POINT_BLIP{worker.CurrentTrashPoint.Id}"
                });
            });

            Alt.Log("Przy stwarzaniu śmietnika ID: " + worker.CurrentTrashPoint.Id);
            worker.AccountEntity.Player.CallNative("addPointToGpsCustomRoute", new object[] { worker.CurrentTrashPoint.TrashPosition.X, worker.CurrentTrashPoint.TrashPosition.Y, worker.CurrentTrashPoint.TrashPosition.Y });
            worker.AccountEntity.Player.CallNative("setGpsMultiRouteRender", new object[] { true });

            worker.AccountEntity.Player.SendChatMessageInfo("Zaznaczono na mapie nowy kosz do opróżnienia. Udaj się do niego, aby zarobić pieniądze!");

        }

        public void Dispose(CharacterEntity worker)
        {
            if (worker.CurrentTrashPoint != null)
            {
                worker.AccountEntity.Player.RemoveDrawText($"JUNKER_TRASH_POINT_DRAW_TEXT{worker.CurrentTrashPoint.Id}");
                Task.Run(async () =>
                {
                    await worker.AccountEntity.Player.DeleteBlip($"JUNKER_TRASH_POINT_BLIP{worker.CurrentTrashPoint.Id}");
                });

                Alt.Log("Przy zamykaniu śmietnika ID: " + worker.CurrentTrashPoint.Id);
                worker.AccountEntity.Player.CallNative("clearGpsMultiRoute");

                worker.CurrentTrashPoint = null;
            }
        }
    }
}
