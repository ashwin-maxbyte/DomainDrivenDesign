namespace DDDWebApi.Models.Configuration
{
    public class ConfigurationResource
    {
        public long? Id { get; set; }
        public decimal RoomTemperature { get; set; }
        public decimal Area { get; set; }
        public decimal CeilingHeight { get; set; }
        public decimal CurrentRating { get; set; }
    }
}
