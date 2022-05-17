using PlayStationClub.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Areas.Services.Interfaces
{
    public interface ISessionService : IService<Session>
    {
        Task<IQueryable<Session>> GetAllSessionsUserAsyn(string userId);
    }
}
