﻿using LSG.DAL.Database;
using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class GroupRepository : GenericRepository, IGroupRepository
    {
        private readonly RoleplayContext _context;

        public GroupRepository(RoleplayContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<GroupModel>> GetAll()
        {
            List<GroupModel> groups = await _context.Groups
                .Include(w => w.Workers)
                .Include(v => v.Vehicles)
                .ToListAsync();

            return groups;
        }
    }
}