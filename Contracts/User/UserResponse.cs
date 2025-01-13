namespace HirePlatform.Contracts.User
{
    public record UserResponse(
     string Id,
     string FirstName, 
     string LastName,
     string NationalID,
     string Email,
     string Password,
     string PhoneNumber,
     DateTime CreatedDate
    );
}
