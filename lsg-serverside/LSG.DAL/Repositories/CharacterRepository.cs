﻿using LSG.DAL.Database;
using LSG.DAL.Database.Models;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class CharacterRepository : GenericRepository<Character>, ICharacterRepository
    {
        private readonly RoleplayContext _context;

        public CharacterRepository(RoleplayContext context) : base (context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAccountCharacters(int id)
        {
            var characters = await _context.Characters.Where(x => x.Id == id).Include(a => a.Account).ToListAsync();

            return characters;
        }
    }
}
