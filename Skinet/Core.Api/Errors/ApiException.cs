using Microsoft.AspNetCore.Mvc;

namespace Skinet.Errors
{
    public class ApiException : ApiResponse
    {
        public string Details { get; set; }
        public ApiException(int statusCode, string title = null, string details = null) : base(statusCode, title)
        {
            Details = details;
        }
        
        
    }
}
