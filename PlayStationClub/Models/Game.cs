using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStationClub.Data.Entity
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte PlayersNumber { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
