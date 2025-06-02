namespace PixCharge.API.Services
{
    public interface IAuthService
    {
        string ComputeHash(string password);
        string GenerateToken(Guid userId, string email, string role);
        Guid GetUserIdFromToken(string token);
    }
}