using PlayStationClub.Data.Entity;
using System;
using System.Collections.Generic;

namespace PlayStationClub.Infrastructure.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public int SessionId { get; set; }
        public ICollection<SessionViewModel> Sessions { get; set; }
    }
}
