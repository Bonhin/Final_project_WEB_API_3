namespace Final_project_WEB_API_3.Utilits
{
    public class TokenConfiguration
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public string Issuer { get; set; }
        public int ExpirationTimeInHours { get; set; }
        public string Module { get; set; }
    }
}
