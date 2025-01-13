namespace HirePlatform.Contracts.Authorization
{
    public record LoginResponse(
        string Id,
        string? Email,
        string FirstName,
        string LastName,
        string Token,
        int Expiresln
        );
    
    
}
