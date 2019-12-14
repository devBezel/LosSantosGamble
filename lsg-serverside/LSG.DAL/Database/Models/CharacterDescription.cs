using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LSG.DAL.Database.Models
{
    public class CharacterDescription
    {
        public int Id { get; set; }
        public Character Character { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

    }
}
