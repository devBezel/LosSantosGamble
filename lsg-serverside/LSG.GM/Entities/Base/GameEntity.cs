using AltV.Net;
using LSG.GM.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Base
{
    public abstract class GameEntity : IGameEntity
    {
        public virtual void Spawn()
        {
            Alt.Log($"[{nameof(GameEntity)} [{DateTime.Now}] Entity spawned");
        }

        public abstract void Dispose();
    }
}
