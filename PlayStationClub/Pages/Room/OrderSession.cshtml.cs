using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Data;
using PlayStationClub.Data.Entity;
using PlayStationClub.Infrastructure.ViewModels;

namespace PlayStationClub.Pages.Room
{
    public class OrderSessionModel : PageModel
    {
        private readonly ISessionService _sessionService;
        private readonly UserManager<PlayStationClubUser> _userManager;
        public IQueryable<Session> Sessions { get; set; }
        public SessionViewModel SessionViewModel { get; set; }
        public OrderSessionModel(ISessionService sessionService,UserManager<PlayStationClubUser> userManager)
        {
            _sessionService = sessionService;
            _userManager = userManager;
        }

        public async Task OnGetAsync(int roomId)
        {
            Sessions = await _sessionService.GetAllSessionRoomAsync(roomId);
            SessionViewModel = new SessionViewModel { RoomId = roomId, Sessions = Sessions.ToList() };
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
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            DateTime dateTime = date + time;
            TimeSpan timeSpan = new(duration, 0, 0);
            Session = new Session
            {
                DateTime = dateTime,
                Duration = timeSpan,
                PlayerNumber = 1,
                RoomId = roomId,
                PlayStationClubUserId = user.Id,
                ReviewId = null
            };
            await _sessionService.CreateAsync(Session);

            return RedirectToPage("/Index");
        }
    }
}
