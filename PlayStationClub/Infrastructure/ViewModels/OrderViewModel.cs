using PlayStationClub.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Infrastructure.ViewModels
{
    public class OrderViewModel
    {
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public byte PlayerNumber { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}
