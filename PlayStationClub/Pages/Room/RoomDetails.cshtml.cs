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
    public class RoomDetailsModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        public RoomViewModel Room { get; set; }

        [BindProperty]
        public OrderViewModel Order { get; set; }

        public RoomDetailsModel(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }
        public async Task OnGet(int id)
        {
            Room = _mapper.Map<RoomViewModel>(await _roomService.GetByIdAsync(id));
        }

        public void OnPostOrderSession()
        {

        }
    }
}
