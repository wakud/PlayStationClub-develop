using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlayStationClub.Data;
using PlayStationClub.Data.Entity;
using PlayStationClub.Infrastructure.ViewModels;

namespace PlayStationClub.Pages.Room
{
    public class OrderSessionModel : PageModel
    {
        private readonly PlayStationClubDbContext _context;
        public IQueryable<Session> Sessions { get; set; }
        public SessionViewModel SessioViewModel { get; set; }
        public OrderSessionModel(PlayStationClubDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int roomId)
        {
            //TODO: переробити через сервіси
            Sessions = _context.Sessions.Where(session => session.RoomId == roomId && session.DateTime >= DateTime.Now);
            SessioViewModel = new SessionViewModel { RoomId = roomId, Sessions = Sessions.ToList() };
            return Page();
        }

        [BindProperty]
        public Session Session { get; set; }

        public async Task<IActionResult> OnPostAsync(DateTime date, int duration, TimeSpan time, int roomId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string userName = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            DateTime dateTime = date + time;
            TimeSpan timeSpan = new(duration, 0, 0);
            Session session = new Session
            {
                DateTime = dateTime,
                Duration = timeSpan,
                PlayerNumber = 1,
                RoomId = roomId,
                PlayStationClubUserId = user.Id,
                ReviewId = null
            };
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
