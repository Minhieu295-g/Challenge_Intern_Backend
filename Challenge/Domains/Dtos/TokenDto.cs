namespace DotNetTraining.Domains.Dtos
{
    public class TokenDto
    {
        public string RefreshToken { get; set; }

        public Guid JwtId { get; set; }

        public Guid UserId { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } 

        public string RefreshToken { get; set; }

    }
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }

}
