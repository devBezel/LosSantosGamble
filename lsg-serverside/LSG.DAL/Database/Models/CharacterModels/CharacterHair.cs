using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.CharacterModels
{
    public class CharacterHair
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int HairId { get; set; }
        public int ColorId { get; set; }
    }
}
