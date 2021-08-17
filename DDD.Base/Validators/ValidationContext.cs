namespace DDD.Base.Validators
{
    public class ValidationContext
    {
        public AbstractUserValidator UserValidator { get; }
        public AbstractRoomValidator RoomValidator { get; }
        public AbstractConfigurationValidator ConfigurationValidator { get; set; }

        public ValidationContext(
            AbstractUserValidator userValidator, 
            AbstractRoomValidator roomValidator, 
            AbstractConfigurationValidator configurationValidator)
        {
            UserValidator = userValidator;
            RoomValidator = roomValidator;
            ConfigurationValidator = configurationValidator;
        }
    }
}
