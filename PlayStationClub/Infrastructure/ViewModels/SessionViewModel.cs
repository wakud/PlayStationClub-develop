using PlayStationClub.Data.Entity;
using System;
using System.Collections.Generic;

namespace PlayStationClub.Infrastructure.ViewModels
{
    public class SessionViewModel
    {
        public DateTime? DateTime { get; set; }
        public int? Duration { get; set; }
        public byte PlayerNumber { get; set; }
        public int? RoomId { get; set; }
        public string PlayStationClubUserId { get; set; }
        public int? ReviewId { get; set; }
        public string? RoomName { get; set; }


        public List<Session> Sessions { get; set; }
    }
}
