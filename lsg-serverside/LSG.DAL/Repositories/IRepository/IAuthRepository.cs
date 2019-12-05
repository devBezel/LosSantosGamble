using LSG.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories.IRepository
{
    public interface IAuthRepository
    {
        Task<Account> Login(string username, string password);
        Task<Account> Register(Account account, string password);
        Task<bool> UserExists(string username);
    }
}
