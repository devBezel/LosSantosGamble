﻿using LSG.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface ICharacterRepository : IGenericRepository<Character>
    {
        Task<IEnumerable<Character>> GetAccountCharacters(int id);
    }
}
