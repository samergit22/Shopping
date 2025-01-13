using HirePlatform.Authentication;
using HirePlatform.Contracts.Authorization;
using HirePlatform.Contracts.Email;
using HirePlatform.Contracts.User;
using HirePlatform.Helpers.Emails;
using HirePlatform.IServices;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;

namespace HirePlatform.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private IJwtProvider _jwtprovider;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailServices _emailServices;

        public UserService(UserManager<User> userManager 
            , IJwtProvider jwtProvider 
            ,SignInManager<User> signInManager
            , IEmailServices emailServices)
        {
            _userManager = userManager;   
            _jwtprovider = jwtProvider;
            _signInManager = signInManager;
            _emailServices = emailServices;
        }
     
        public async Task<IEnumerable<User>> GetUsers()
        { 
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User?> GetUser(string Id)
        {
            
            return  await _userManager.FindByIdAsync(Id);

        }


        [HttpPost]
        public async Task<UserResponse> Add(UserRequest user , string password)
        {

            var newuser = user.Adapt<User>();

            newuser.UserName = user.Email.Split("@")[0];

            var result = await _userManager.CreateAsync(newuser , password);
            if (result.Succeeded)
            {
                var userResponse = newuser.Adapt<UserResponse>();
                return userResponse;
            }
            
            return null;
        }

        public  async Task<bool> DeleteAsync(string Id ,CancellationToken cancellationToken =  default)
        {
            var user = await GetUser(Id);

            if (user is null)
                return false;

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }


        public async Task<bool> UpdateAsync(string Id, User user, CancellationToken cancellationToken = default)
        {
            var existingUser = await GetUser(Id);
            if (existingUser is null)
                return false;

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.UserName = user.Email.Split("@")[0];

            var result = await _userManager.UpdateAsync(existingUser);
            return result.Succeeded;
        }

        //Login
        async Task<LoginResponse> IUserService.GetTokenAsync(string email, string password, CancellationToken cancellationToken)
        {
            //check user?
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return null;
            }
            //check password
            var isvalidpassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isvalidpassword)
                return null;
            //generate JWT token
            var (token, expiresIn) = _jwtprovider.GenerateToken(user);
            //return new UserResponse
            return new LoginResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn);
        }

        public async Task<IActionResult> AuthnticateEmail(EmailRequest email)
        {
            var user = await _userManager.FindByEmailAsync(email.Email);
            if (user == null)
            {
                return new NotFoundObjectResult(new { message = "User not found" });
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>()
            {
              {"token",token },
              {"email",email.Email}
            };
            var callback = QueryHelpers.AddQueryString(email.ClientUrl, param);
            var displayName = $"{user.FirstName} {user.LastName}";
            var confirmEmail = new ConfirmEmail(email.Email, displayName, callback);
       
            try
            {
                await _emailServices.SendEmail(confirmEmail);
                return new OkObjectResult(new { message = "Email confirmation link sent successfully." });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = "Failed to send email", error = ex.Message })
                {
                    StatusCode = 500
                };
            }
        }
        public async Task<IEnumerable<User>> FindUsersByNameAsync(string name)
        {
            // This is assuming you have a Users table or collection in your DbContext
            return await _userManager.Users
                                 .Where(u => (u.FirstName.ToLower() + " " + u.LastName.ToLower()).Contains(name)) 
                                 .ToListAsync();
        }
    }
}
