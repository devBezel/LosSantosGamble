using AltV.Net;
using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Base
{
    public abstract class GameEntity : IGameEntity
    {
        public virtual void Spawn(IPlayer player)
        {
            Alt.Log($"[{nameof(GameEntity)} [{DateTime.Now}] Entity spawned for Player {player.Name}");
        }
        public abstract void Dispose();
    }
}
