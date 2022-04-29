using System;
using System.Collections.Generic;

namespace PlayStationClub.Data.Entity
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public byte PlayerNumber { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public string PlayStationClubUserId { get; set; }
        public PlayStationClubUser PlayStationClubUser { get; set; }

        public int? ReviewId { get; set; }
        public Review Review { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
