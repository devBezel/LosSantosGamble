using AltV.Net.Data;
using LSG.GM.Entities.Core.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Helpers.Models
{
    public class ItemInWorldModel
    {
        public ItemEntity ItemEntity { get; set; }
        public Position Position { get; set; }
        public int Dimension { get; set; }
    }
}
