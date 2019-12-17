using LSG.BLL.Dto.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.BLL.Dto.Character
{
    public class CharacterForGuestDto
    {
        public AccountForCharacterDto Account { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public string Description { get; set; }
        public string History { get; set; }
        public string PicUrl { get; set; }
    }
}
