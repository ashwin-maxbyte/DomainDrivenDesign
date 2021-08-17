namespace DDD.Base.Models
{
    public class Configuration : BaseEntity
    {
        public User User { get; set; }
        public long UserId { get; set; }
        public decimal RoomTemperature { get; set; }
        public decimal Area { get; set; }
        public decimal CeilingHeight { get; set; }
        public decimal CurrentRating { get; set; }
    }
}
