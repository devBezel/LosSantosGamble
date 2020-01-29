using LSG.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Entities.Core.Buidling
{
    public static class BuildingFactory
    {
        public static string CreateName(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.Apartament: return "Mieszkanie";
                case BuildingType.House: return "Dom jednorodzinny";
                default:
                    return "Nieznane";
            }
        }

        public static int CreateBlip(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.Apartament: return 369;
                case BuildingType.House: return 40;
                default:
                    return 0;
            }
        }
    }
}
