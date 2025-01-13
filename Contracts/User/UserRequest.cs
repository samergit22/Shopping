namespace HirePlatform.Contracts.User
{
    public record UserRequest(
     string FirstName,
     string LastName,
     string Email,
     string Password,
     string NationalID,
     string PhoneNumber,
     DateTime CreatedDate
    );
}
 