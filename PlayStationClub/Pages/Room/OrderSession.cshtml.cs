using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly PlayStationClubDbContext _context;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        public ICollection<OrderViewModel> OrderViews { get; set; }
        public OrderViewModel OrderViewModel { get; set; }
        public OrderSessionModel(PlayStationClubDbContext context, IMapper mapper, ISessionService sessionService)
        {
            _context = context;
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Session Session { get; set; }

        public async Task<IActionResult> OnPostAsync(int roomId, DateTime date, TimeSpan time, int duration)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string userName = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            DateTime dateTime = date + time;
            TimeSpan timeSpan = new (duration, 0, 0);
            var session = new Session
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

            return RedirectToPage("./Rooms");
        }
        
    }
}
