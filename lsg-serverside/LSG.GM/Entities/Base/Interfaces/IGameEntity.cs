using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Base.Interfaces
{
    public interface IGameEntity
    {
        void Spawn(IPlayer player);
        void Dispose();
    }
}
