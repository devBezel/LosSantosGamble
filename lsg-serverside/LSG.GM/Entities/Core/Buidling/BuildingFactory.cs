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
                case BuildingType.Staircase: return "Blok mieszkalny";
                default:
                    return "Nieznane";
            }
        }

        public static int CreateBlip(BuildingType buildingType, bool onSale)
        {
            switch (buildingType)
            {
                case BuildingType.Apartament:
                    if (onSale) return 369; return 411;
                case BuildingType.House:
                    if(onSale) return 375; return 40;
                default:
                    return 0;
            }
        }

        public static int CreateColor(bool onSale)
        {
            if (onSale)
            {
                return 25;
            }
            else
            {
                return 49;
            }
        }
    }
}
