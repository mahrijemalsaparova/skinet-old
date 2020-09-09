namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        // DeveloperExceptionPage'deki hataları da ApiResponse'de döndürmek istiyoruz.
        public ApiException(int statusCode, string message = null, string details = null ) : base(statusCode, message)
        {
            Details = details;
        }
        // Details = Exception hatası 
        public string Details { get; set; }
    }
}