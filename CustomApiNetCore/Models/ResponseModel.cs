using Microsoft.AspNetCore.Mvc;

namespace CustomApiNetCore.Models
{
    public class ResponseModel
    {
        public ResponseModel() { }
        public string message { get; set; } = string.Empty;
        public bool success { get; set; } = false;
        public object? data { get; set; } = null;
        public int code { get; set; } = 200;
    }
}
