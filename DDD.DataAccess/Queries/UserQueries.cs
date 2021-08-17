using DDD.Base.DataManagement;
using DDD.Base.Models;
using DDD.Base.Queries;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DDD.DataAccess.Queries
{
    public class UserQueries : IUserQueries
    {
        private readonly IRepository _repository;

        public UserQueries(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetUser(string email, string password)
        {
            return await _repository.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task<User> GetUser(string email)
        {
            return await _repository.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
