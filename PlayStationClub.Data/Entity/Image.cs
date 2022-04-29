namespace PlayStationClub.Data.Entity
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public Game Game { get; set; }

        public int? RoomId { get; set; }
        public Room Room { get; set; }
    }
}
