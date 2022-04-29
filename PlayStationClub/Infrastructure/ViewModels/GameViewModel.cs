using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayStationClub.Infrastructure.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte PlayersNumber { get; set; }
        public int ImageId { get; set; }
        public ImageViewModel Image { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }
    }
}
