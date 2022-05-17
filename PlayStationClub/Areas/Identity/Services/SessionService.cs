using Microsoft.EntityFrameworkCore;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Data;
using PlayStationClub.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Areas.Services
{
    public class SessionService : GenericService<Session>, ISessionService
    {
        public SessionService(PlayStationClubDbContext context):base(context)
        {
        }

        //все сесии по юзеру
        public async virtual Task<IQueryable<Session>> GetAllSessionsUserAsync(string userId)
        {
            return await Task.FromResult(dbContext.Sessions.Include(s => s.Room).Include(s => s.Review)
                .Where(s => s.PlayStationClubUserId == userId));
        }
        
        //все сеии по комнате
        public async virtual Task<IQueryable<Session>> GetAllSessionRoomAsync(int roomId)
        {
            return await Task.FromResult(dbContext.Sessions.Include(s => s.Room).Include(s => s.Review)
                .Where(s => s.RoomId == roomId && s.DateTime >= DateTime.Now));
        }
    }
}
