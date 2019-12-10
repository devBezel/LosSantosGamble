using LSG.DAL.Database.Models;
using LSG.DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICharacterRepository CharacterRepository { get; set; }
    }
}
