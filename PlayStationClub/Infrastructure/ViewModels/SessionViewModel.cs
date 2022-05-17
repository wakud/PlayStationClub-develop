using PlayStationClub.Data.Entity;
using System;
using System.Collections.Generic;

namespace PlayStationClub.Infrastructure.ViewModels
{
    public class SessionViewModel
    {
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
        public int? Duration { get; set; }
        public int? RoomId { get; set; }
        
        public List<Session> Sessions { get; set; }
    }
}
