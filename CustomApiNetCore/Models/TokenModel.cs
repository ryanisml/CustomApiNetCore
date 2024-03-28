namespace CustomApiNetCore.Models
{
    public class TokenModel
    {
        public string token { get; set; } = string.Empty;
        public DateTime createdAt { get; set; }
        public DateTime expiredAt { get; set; }
    }
}
