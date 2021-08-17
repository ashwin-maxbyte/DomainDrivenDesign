namespace DDDWebApi.Models
{
    public class GenericResponse<T>
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public T Payload { get; private set; }

        private GenericResponse()
        {
        }

        public static GenericResponse<T> GetSuccessResponse(T payload)
        {
            return new GenericResponse<T>
            {
                IsSuccess = true,
                Payload = payload
            };
        }

        public static GenericResponse<T> GetFailureResponse(string message)
        {
            return new GenericResponse<T>
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
