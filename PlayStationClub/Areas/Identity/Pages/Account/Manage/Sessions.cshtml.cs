using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Data;
using PlayStationClub.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PlayStationClub.Areas.Identity.Pages.Account.Manage
{
    public class SessionsModel : PageModel
    {
        private readonly ISessionService _sessionService;
        private readonly UserManager<PlayStationClubUser> _userManager;
        public SessionsModel(ISessionService sessionService,UserManager<PlayStationClubUser> userManager)
        {
            _sessionService = sessionService;
            _userManager = userManager;
        }
        public IQueryable<Session> Sessions { get; set; }
        [BindProperty]
        public Session Session { get; set; }

        public async Task OnGet()
        {
            string userName = User.Identity.Name;
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            Sessions = await _sessionService.GetAllSessionsUserAsyn(user.Id);
        }

        //відмова від сеансу
        public async Task<IActionResult> OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session = await _sessionService.GetByIdAsync((int)id);
            if (Session != null)
            {
                await _sessionService.DeleteAsync(Session);
            }

            return RedirectToPage("./Sessions");
        }
    }
}
