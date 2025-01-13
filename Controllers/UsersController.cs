using HirePlatform.Contracts.Authorization;
using HirePlatform.Contracts.Email;
using HirePlatform.Contracts.User;
using HirePlatform.IServices;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;

namespace HirePlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController(IUserService userService , IEmailServices emailServices) : ControllerBase
    {
        private readonly IEmailServices _emailServices = emailServices;

        //That's Services
        private readonly IUserService _userService = userService;

        [HttpGet("All Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            var response = users.Adapt<IEnumerable<UserResponse>>();
            return Ok(response);    
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUser([FromRoute]string Id)
        {
            var users = await _userService.GetUser(Id);
            if (users == null)
                return NotFound(new { message = "User Not founded???????????"});
            var response = users.Adapt<UserResponse>();
            return Ok(response);
 
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Add([FromBody] UserRequest userRequest)
        {
            if (string.IsNullOrEmpty(userRequest.Email) || string.IsNullOrEmpty(userRequest.Password))
            {
                return BadRequest("Email and password are required.");
            }


            if (userRequest == null)
                return BadRequest(new { message = "Invalid user data" });

            try
            {
                var createdUser = await _userService.Add(userRequest, userRequest.Password);
                if (createdUser == null)
                    return BadRequest();



                return Ok(createdUser); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "User registration failed", error = ex.Message });
            }
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LoginAsync(LoginRequest request , CancellationToken cancellationToken)
        {
            var authResult = await _userService.GetTokenAsync(request.Email , request.Password , cancellationToken);
            return authResult is null ? BadRequest("Invalid Email/ Password") : Ok(authResult);
        }

        //Need page for confirm email(front) [Url , email]

        [HttpPost("authnticate-email")]
        public async Task<ActionResult> AuthnticateEmail([FromBody] EmailRequest checkEmail)
        {
            if (checkEmail == null || string.IsNullOrEmpty(checkEmail.Email))
            {
                return BadRequest(new { message = "Invalid email." });
            }

            var result = await _userService.AuthnticateEmail(checkEmail);

            if (result is OkObjectResult)
            {
                return Ok(new { message = "A verification link has been sent to your email." });
            }
            else
            {
                return BadRequest(new { message = "Failed to send verification email." });
            }
        }


        [HttpPut("{id}Update")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UserRequest request,
        CancellationToken cancellationToken)
        {
            if (request == null)
                return BadRequest(new { message = "Invalid user data" });
            if (id == null)
                return BadRequest(new { message = "Invalid user data" });
            var isUpdated = await _userService.UpdateAsync(id, request.Adapt<User>(), cancellationToken);

            if (!isUpdated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id, [FromQuery] bool confirm = false, CancellationToken cancellationToken = default)
        {
            if (!confirm)
                return BadRequest(new { message = "Delete confirmation is required. Pass 'confirm=true' in the query string." });

            var isDeleted = await _userService.DeleteAsync(id, cancellationToken);

            if (!isDeleted)
                return NotFound();

            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> FindUsersByName([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name parameter is required.");
            }

            var users = await _userService.FindUsersByNameAsync(name);

            if (users == null || !users.Any())
            {
                return NotFound("No users found matching the given name.");
            }
            var response = users.Adapt<IEnumerable<UserResponse>>();
            return Ok(response);
        }


    }
}
