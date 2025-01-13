using Shopping.Models;

namespace HirePlatform.Authentication
{
    public interface IJwtProvider
    {
        (string Token, int ExpireIn) GenerateToken(User user);
        
    }
}
