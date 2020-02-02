using LSG.DAL.Database.Models;
using LSG.DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; set; }
        ICharacterRepository CharacterRepository { get; set; }
        IVehicleRepository VehicleRepository { get; set; }
        IAtmRepository AtmRepository { get; set; }
        IBusRepository BusRepository { get; set; }
        IBuildingRepository BuildingRepository { get; set; }
        IItemRepository ItemRepository { get; set; }
    }
}
