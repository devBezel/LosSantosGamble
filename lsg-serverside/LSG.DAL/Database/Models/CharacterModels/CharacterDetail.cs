using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models.CharacterModels
{
    public class CharacterDetail
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int CharacterFaceId{ get; set; }
        public CharacterFace CharacterFace { get; set; }
    }
}
