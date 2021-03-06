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
        public SessionViewModel SessionVM { get; set; }
        [BindProperty]
        public Session Session { get; set; }

        public async Task OnGet(int id)
        {
            var room = _mapper.Map<RoomViewModel>(await _roomService.GetByIdAsync(id));
            Room = new RoomViewModel{ 
                Id = room.Id,
                Images = room.Images.Reverse().Take(3).Reverse(),
                Description = room.Description,
                Name = room.Name,
                PlayersNumber = room.PlayersNumber,
                Price = room.Price
            };
        }

        public async Task<PartialViewResult> OnGetOrderSessionAsync(int roomId)
        {
            Sessions = await _sessionService.GetAllSessionRoomAsync(roomId);
            Room = _mapper.Map<RoomViewModel>(await _roomService.GetByIdAsync(roomId));
            Dictionary<string, List<string>> dictionary = new();
            foreach (var session in Sessions)
            {
                if (!dictionary.TryGetValue(session.DateTime.Date.ToString("d"), out List<string> list))
                {
                    list = new List<string>();
                    dictionary.Add(session.DateTime.Date.ToString("d"), list);
                }
                var sessionStart = session.DateTime.TimeOfDay;
                var sessionEnd = session.DateTime.TimeOfDay + session.Duration;
                list.Add(sessionStart.ToString(@"hh\:mm"));
                list.Add(sessionEnd.ToString(@"hh\:mm"));
            }
            SessionVM = new SessionViewModel { RoomId = roomId, DictionarySessions = dictionary, PlayerNumber = Room.PlayersNumber };
            return new PartialViewResult
            {
                ViewName = "OrderSession",
                ViewData = new ViewDataDictionary<SessionViewModel>(ViewData, SessionVM)
            };
        }

        public async Task<IActionResult> OnPostOrderSessionAsync(DateTime date, int duration, TimeSpan time, int roomId, byte players)
        {
            string userName = User.Identity.Name;
            var user = _userManager.Users.FirstOrDefault(u => u.UserName == userName);
            Room = _mapper.Map<RoomViewModel>(await _roomService.GetByIdAsync(roomId));
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
            string message = $"???? ?????????????????????????? ???????????? ?? PlayStation ?????????? GSTICK ?????????? ???? " + Session.DateTime.Date.ToString("d") +
                " ?? " + Session.DateTime.TimeOfDay.ToString(@"hh\:mm") + ". ?????????????? " + Room.Name;
            
            await _sessionService.CreateAsync(Session);
            await _emailSender.SendEmailAsync(user.Email, "???????????????????????? ???????????? ?????????????????? ??????????????.", message);

            return RedirectToPage("./RoomDetails");
        }
    }
}
