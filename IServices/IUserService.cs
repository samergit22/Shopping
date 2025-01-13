using HirePlatform.Contracts.Authorization;
using HirePlatform.Contracts.Email;
using HirePlatform.Contracts.User;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;

namespace HirePlatform.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(string Id);
        Task<UserResponse> Add(UserRequest user, string password);
        Task<bool> UpdateAsync(string Id, User user , CancellationToken cancellationToken = default);
       Task<bool> DeleteAsync(string Id, CancellationToken cancellationToken = default);
        Task<LoginResponse> GetTokenAsync(string email, string password, CancellationToken cancellationToken);
        Task<IActionResult> AuthnticateEmail(EmailRequest email);
        Task<IEnumerable<User>> FindUsersByNameAsync(string name);
    }
}