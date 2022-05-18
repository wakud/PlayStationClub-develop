using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Data;
using PlayStationClub.Data.Entity;
using PlayStationClub.Infrastructure.ViewModels;

namespace PlayStationClub.Pages.Room
{
    public class RoomDetailsModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly UserManager<PlayStationClubUser> _userManager;
        private readonly IEmailSender _emailSender;
        public RoomViewModel Room { get; set; }
        [BindProperty]
        public OrderViewModel Order { get; set; }
        public ICollection<OrderViewModel> OrderViews { get; set; }
        public RoomDetailsModel(
            IRoomService roomService,
            IMapper mapper,
            ISessionService sessionService,      
            UserManager<PlayStationClubUser> userManager,
            IEmailSender emailSender
            )
        {
            _roomService = roomService;
            _mapper = mapper;
            _sessionService = sessionService;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public IQueryable<Session> Sessions { get; set; }
        public SessionViewModel SessionViewModel { get; set; }

        [BindProperty]
        public Session Session { get; set; }

        public async Task OnGet(int id)
        {
            Room = _mapper.Map<RoomViewModel>(await _roomService.GetByIdAsync(id));
        }

        public async Task<PartialViewResult> OnGetOrderSessionAsync(int roomId)
        {
            Sessions = await _sessionService.GetAllSessionRoomAsync(roomId);
            SessionViewModel = new SessionViewModel { RoomId = roomId, Sessions = Sessions.ToList() };
            return new PartialViewResult
            {
                ViewName = "OrderSession",
                ViewData = new ViewDataDictionary<SessionViewModel>(ViewData, SessionViewModel)
            };
        }

        public async Task<IActionResult> OnPostOrderSessionAsync(DateTime date, int duration, TimeSpan time, int roomId)
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

            string message = $"Вы забронировали комнату, игра состоится: " + Session.DateTime.Date.ToString("d") +
                " в " + Session.DateTime.TimeOfDay.ToString(@"hh\:mm") + " продолжительность " + Session.Duration.ToString(@"hh\:mm") + " часа";
            
            await _sessionService.CreateAsync(Session);
            await _emailSender.SendEmailAsync(user.Email, "Бронирование сеанса совершено успешно.", message);

            return RedirectToPage("/Index");
        }
    }
}
