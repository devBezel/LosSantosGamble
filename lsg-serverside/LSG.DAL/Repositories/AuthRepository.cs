using LSG.DAL.Database;
using LSG.DAL.Database.Models;
using LSG.DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly RoleplayContext _context;

        public AuthRepository(RoleplayContext context)
        {
            _context = context;
        }

        public async Task<Account> Login(string username, string password)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Username == username);
            Console.WriteLine($"authrep: {account.Username}");
            if (account == null)
                return null;

            if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
                return null;

            return account;
        }

        public async Task<Account> Register(Account account, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHashSalt(password, out passwordHash, out passwordSalt);

            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Accounts.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }

                return true;
            }
        }
    }
}
