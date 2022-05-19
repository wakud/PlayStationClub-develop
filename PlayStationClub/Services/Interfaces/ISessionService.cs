using PlayStationClub.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Areas.Services.Interfaces
{
    public interface ISessionService : IService<Session>
    {
        Task<IQueryable<Session>> GetAllSessionsUserAsync(string userId);
        Task<IQueryable<Session>> GetAllSessionRoomAsync(int roomId);
    }
}
