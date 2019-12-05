﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Rank { get; set; }
    }
}
