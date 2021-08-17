using DDD.Base.Models;
using System.Threading.Tasks;

namespace DDD.Base.Queries
{
    public interface IUserQueries
    {
        Task<User> GetUser(string email, string password);

        Task<User> GetUser(string email);
    }
}
