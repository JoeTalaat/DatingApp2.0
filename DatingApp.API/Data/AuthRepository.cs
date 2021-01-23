using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly  DataContext _dataContext;
        public AuthRepository(DataContext dataContext)
        {
            _dataContext=dataContext;
        }
        public async Task<User> Login(string UserName, string Password)
        {
            var User = await _dataContext.Users.FirstOrDefaultAsync(e=>e.Username==UserName);
            if(User==null)        
                return null;

            if(!VerifyPasswordHash(Password,User.PasswordSalt,User.PasswordHash))
            {
                return null;
            }

            return User;
            
        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
            
                var ComputedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); 
                for(int i=0; i<ComputedHash.Length;i++)
                {
                    if(ComputedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
                
            }

            return true;
        }

        public async Task<User> Register(User user, string Password)
        {
            byte[] passwordHash , passwordSalt;
            CreatePasswordHash(Password,out passwordHash,out passwordSalt);
            user.PasswordHash=passwordHash;
            user.PasswordSalt=passwordSalt;
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt=hmac.Key; // the randomly generated key that we are about to add it to the hashed password

                //converting our string password to array of bytes in order to generate a hashed password out of the text password

                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); 

            }
            
        }

        public async Task<bool> UserExists(string UserName)
        {
            if(await _dataContext.Users.AnyAsync(e=>e.Username==UserName))
            {
                return true;
            }

            return false;
        }
    }
}