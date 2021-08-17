using DDD.Base.Models;
using System.Linq;

namespace DDD.Base.DataManagement
{
    public interface IRepository
    {
        IQueryable<User> Users { get; }

        IQueryable<Configuration> Configurations { get; }

        IQueryable<Room> Rooms { get; }
    }
}
