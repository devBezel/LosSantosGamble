using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Events
{
    public class OnColshapeEnter : IScript
    {
        //public OnColshapeEnter()
        //{
        //    Alt.OnColShape += OnEnterColshape;
        //}

        [ScriptEvent(ScriptEventType.ColShape)]
        public void OnEnterColshape(IColShape colShape, IEntity targetEntity, bool state)
        {
            if (targetEntity.Type != BaseObjectType.Player) return;
            IPlayer player = targetEntity as IPlayer;

            if (state == false)
            {
                Interaction.Clear(player);
            }
        }
    }
}
