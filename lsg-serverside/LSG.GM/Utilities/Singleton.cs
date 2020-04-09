using AltV.Net;
using AutoMapper;
using LSG.BLL.Mappers;
using LSG.DAL.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Utilities
{
    public class Singleton
    {

        private static RoleplayContext Database;
        private static Mapper mapper;

        public static RoleplayContext GetDatabaseInstance()
        {
            if(Database == null)
            {
                Database = new RoleplayContextFactory().Create();
                Alt.Log($"[MYSQL] Poprawnie stworzono polączenie z bazą danych");
            }

            return Database;
        }

        public static IMapper AutoMapper()
        {
            if(mapper == null)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());

                mapper = new Mapper(config);
            }

            return mapper;
        }

    }
}
