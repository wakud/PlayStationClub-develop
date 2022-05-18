using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStationClub.Data.Entity
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [Required]
        public byte Rating { get; set; }
        public DateTime ReceivedDate { get; set; }

        public Session Session { get; set; }
    }
}
