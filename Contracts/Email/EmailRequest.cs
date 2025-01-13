using Microsoft.Identity.Client;

namespace HirePlatform.Contracts.Email
{
    public record EmailRequest(
        string Email,
        string ClientUrl    
    );
   
}
