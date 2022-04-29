using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayStationClub.Areas.Services.Interfaces;
using PlayStationClub.Infrastructure.ViewModels;

namespace PlayStationClub.Pages.Room
{
    public class RoomsModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public ICollection<RoomViewModel> Rooms { get; set; }

        public RoomsModel(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }
        public async Task OnGet()
        {
            Rooms = _mapper.Map<ICollection<RoomViewModel>>(await _roomService.GetAllAsync());
        }
    }
}
