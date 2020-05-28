using LSG.DAL.Database;
using LSG.DAL.Database.Models;
using LSG.DAL.Repositories;
using LSG.DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RoleplayContext _context;

        public UnitOfWork(RoleplayContext context)
        {
            _context = context;
            AccountRepository = new AccountRepository(_context);
            CharacterRepository = new CharacterRepository(_context);
            VehicleRepository = new VehicleRepository(_context);
            AtmRepository = new AtmRepository(_context);
            BusRepository = new BusRepository(_context);
            BuildingRepository = new BuildingRepository(_context);
            ItemRepository = new ItemRepository(_context);
            ShopRepository = new ShopRepository(_context);
            GroupRepository = new GroupRepository(_context);
            WarehouseRepository = new WarehouseRepository(_context);
            SmartphoneMessageRepository = new SmartphoneMessageRepository(_context);
        }
        public IAccountRepository AccountRepository { get; set; }
        public ICharacterRepository CharacterRepository { get; set; }
        public IVehicleRepository VehicleRepository { get; set; }
        public IAtmRepository AtmRepository { get; set; }
        public IBusRepository BusRepository { get; set; }

        public IBuildingRepository BuildingRepository { get; set; }

        public IItemRepository ItemRepository { get; set; }

        public IShopRepository ShopRepository { get; set; }

        public IGroupRepository GroupRepository { get; set; }

        public IWarehouseRepository WarehouseRepository { get; set; }

        public ISmartphoneMessageRepository SmartphoneMessageRepository { get; set; }

        public void Dispose()
        {
            _context.SaveChanges();
        }
    }
}
