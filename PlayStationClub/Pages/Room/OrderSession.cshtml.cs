using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Data;
using PlayStationClub.Data.Entity;

namespace PlayStationClub.Pages.Room
{
    public class OrderSessionModel : PageModel
    {
        private readonly PlayStationClubDbContext _context;

        public OrderSessionModel(PlayStationClubDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //ViewData["PlayStationClubUserId"] = new SelectList(_context.Users, "Id", "Id");
            //ViewData["ReviewId"] = new SelectList(_context.Reviews, "Id", "Text");
            //ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public Session Session { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            string userName = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            var sessionId = _context.Sessions.Last();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var session = new Session {
                Id = sessionId.Id + 1,
                DateTime = Session.DateTime.Date + Session.DateTime.TimeOfDay,
                Duration = Session.Duration,
                PlayerNumber = 1,
                RoomId = Session.RoomId,
                PlayStationClubUserId = user.Id,
                ReviewId = 1
            };
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
