using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Base.Interfaces
{
    interface IEntityFactory
    {
        public interface IEntityFactory<TEntity, TModel>
        {
            TEntity Create(TModel model);
        }
    }
}
