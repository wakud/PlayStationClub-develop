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
    public class GameService : GenericService<Game>, IGameService
    {
        public GameService(PlayStationClubDbContext context) : base(context)
        {

        }

        public override async Task<ICollection<Game>> GetAllAsync()
        {
            return await dbContext.Games
                .Include(g => g.Image)
                .Include(g=>g.Categories)
                .ToListAsync();
        }

        public override async Task<Game> GetByIdAsync(int id)
        {
            return await dbContext.Games
                .Include(g => g.Image)
                .Include(g => g.Categories)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
