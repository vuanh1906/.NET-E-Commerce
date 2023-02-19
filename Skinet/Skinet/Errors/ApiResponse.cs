namespace Skinet.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            status = statusCode;
            title = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

   
        public int status { get; set; }
        public string title { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Error are the path to the dark side.",
                _ => null
            };
        }

    }
}
