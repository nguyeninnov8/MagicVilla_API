using System.Net;

namespace MagicVilla.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessage { get; set; } = new List<string> { "No Error Found." };
        public object? Result { get; set; }
    }
}
