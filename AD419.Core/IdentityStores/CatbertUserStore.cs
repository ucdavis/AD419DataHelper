using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AD419.Core.Models;
using Microsoft.AspNet.Identity;


namespace AD419.Core.IdentityStores
{
    public class CatbertUserStore : IUserStore<User, int>
    {
        private readonly CatbertDataContext _dbContext;

        public CatbertUserStore(CatbertDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
        }

        public Task CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
