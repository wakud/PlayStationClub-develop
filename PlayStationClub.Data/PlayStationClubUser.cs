using Microsoft.AspNetCore.Identity;
using PlayStationClub.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Data
{
    public class PlayStationClubUser : IdentityUser
    {
        [PersonalData]
        public string FullName { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
