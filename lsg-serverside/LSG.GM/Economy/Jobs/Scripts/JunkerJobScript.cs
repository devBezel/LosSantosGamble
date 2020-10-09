using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Economy.Jobs.Base.Junker;
using LSG.GM.Entities.Core;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Economy.Jobs.Scripts
{
    public class JunkerJobScript : IScript
    {
        [ScriptEvent(ScriptEventType.ColShape)]
        public void OnPlayerEnterColshape(IColShape colShape, IEntity entity, bool state)
        {
            if(entity is IPlayer player)
            {
                if(state)
                {
                    if(colShape.HasData("trash:data"))
                    {
                        CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;

                        colShape.GetData("trash:data", out TrashPointModel trashPoint);
                        if(characterEntity.CurrentTrashPoint != null)
                        {
                            if(characterEntity.CurrentTrashPoint.Id == trashPoint.Id)
                            {
                                new Interaction(player, "job-junker:dump", "aby opróżnić kosz na śmieci");
                            }
                        }
                    }
                }
            }
        }

        [ClientEvent("job-junker:dump")]
        public void DumpTrash(IPlayer player)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (characterEntity == null)
                return;

            if(characterEntity.CasualJob is JunkerJob junkerJob)
            {
                junkerJob.Dispose(characterEntity);

                junkerJob.ChargeMoney(characterEntity, 10);

                player.SendChatMessageToNearbyPlayers("opróżnia kosz na śmieci wrzucając zawartość do śmieciarki", ChatType.Me);
                player.SendChatMessageInfo("Otrzymałeś 10$ dolarów za opróżnienie śmietnika");
            }
        }


        //TODO: Losuje punkt jak ktoś wejdzie do pojazdu śmieciarki i go nie ma
        [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(IVehicle vehicle, IPlayer player, byte seat)
        {
            CharacterEntity characterEntity = player.GetAccountEntity().characterEntity;
            if (characterEntity == null)
                return;
            if(characterEntity.CasualJob != null)
            {
                if(characterEntity.CasualJob is JunkerJob junkerJob)
                {
                    if(vehicle == characterEntity.CasualJobVehicle.VehicleEntity.GameVehicle)
                    {
                        if (characterEntity.CurrentTrashPoint == null)
                        {
                            junkerJob.GetTrashPoint(characterEntity);
                        }
                    }
                }
            }
        }
    }
}
