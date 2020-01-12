using AltV.Net.Elements.Entities;
using LSG.GM.Entities.Common.Atm;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Extensions
{
    public static class BankExtenstion
    {
        public static AtmEntity GetAtmEntity(this IColShape colshape)
        {
            colshape.GetData("atm:data", out AtmEntity entity);

            return entity;
        }
    }
}
