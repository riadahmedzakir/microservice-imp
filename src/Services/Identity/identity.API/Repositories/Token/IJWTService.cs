namespace identity.API.Data.Token
{
    public interface IJWTService
    {
        string GenerateToken(string userName, string passWord);
        bool IsTokenValid(string token);
    }
}
