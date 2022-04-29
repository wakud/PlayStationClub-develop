using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStationClub.Data.Entity
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte PlayersNumber { get; set; }
        public decimal Price { get; set; }

        public ICollection<Image> Images { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
