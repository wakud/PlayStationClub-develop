using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Data;
using PlayStationClub.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PlayStationClub.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace PlayStationClub.Areas.Identity.Pages.Account.Manage
{
    public class SessionsModel : PageModel
    {
        private readonly ISessionService _sessionService;
        private readonly UserManager<PlayStationClubUser> _userManager;
        
        public SessionsModel(ISessionService sessionService, UserManager<PlayStationClubUser> userManager, IReviewService reviewService)
        {
            _sessionService = sessionService;
            _userManager = userManager;
            _reviewService = reviewService;
        }
        public IQueryable<Session> Sessions { get; set; }
        [BindProperty]
        public Session Session { get; set; }

        //То для відгуку модального вікна
        private readonly IReviewService _reviewService;
        public ReviewViewModel ReviewView { get; set; }
        
        [BindProperty]
        public Review Review { get; set; } = new Review();

        public async Task OnGetAsync()
        {
            string userName = User.Identity.Name;
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            Sessions = await _sessionService.GetAllSessionsUserAsync(user.Id);
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


        public async Task<PartialViewResult> OnGetReviewAsync(int sessionId)
        {
            Session = await _sessionService.GetByIdAsync(sessionId);
            ReviewView = new ReviewViewModel
            {
                SessionId = sessionId,
                DateTime = Session.DateTime,
            };

            return new PartialViewResult
            {
                ViewName = "Reviews",
                ViewData = new ViewDataDictionary<ReviewViewModel>(ViewData, ReviewView)
            };
        }

        public async Task<IActionResult> OnPostReviewAsync(int sessionsId, DateTime dateTime, string comments, byte starCount)
        {
            Review = new Review
            {
                Rating = starCount,
                ReceivedDate = DateTime.Now,
                Text = comments
            };
            Console.WriteLine(starCount.ToString());
            var rewDb = await _reviewService.CreateAsync(Review);

            Session = await _sessionService.GetByIdAsync(sessionsId);
            Session.ReviewId = rewDb.Id;

            await _sessionService.UpdateAsync(Session);
            return RedirectToPage("./Sessions");
        }
    }
}
