using APIsLayer.DTOs.Identity;
using APIsLayer.Errors;
using APIsLayer.Extensions;
using CoreLayer.Entities.Identity;
using CoreLayer.ServiceContract;
using CoreLayer.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Claims;

namespace APIsLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserApplication> userManager;
        private readonly ITokenServices token;
        private readonly SignInManager<UserApplication> signInManager;

        public AccountController(UserManager<UserApplication> _UserManager, ITokenServices _token,SignInManager<UserApplication>_signInManager)
        {
            userManager = _UserManager;
            token = _token;
            signInManager = _signInManager;
        }
        [HttpPost("Register")]
        public async  Task<ActionResult<UserDtocs>> Register(UserRegisterDto RegisteredUser)
        {
            if(CheckEmailExists(RegisteredUser.Email).Result.Value) { return BadRequest(new APIResponse(400, "Email already exists")); }
            var AddedUser = new UserApplication()
            {
                DisplayName= RegisteredUser.DisplayName,
                Email=RegisteredUser.Email,
                UserName=  RegisteredUser.userName,
                PhoneNumber=RegisteredUser.PhoneNumber
            };
            var result = await userManager.CreateAsync(AddedUser, RegisteredUser.Password);
            if (!result.Succeeded) { return BadRequest(new APIResponse(400)); }
            var ReturnedUser = new UserDtocs()
            {
                DiaplyName = AddedUser.DisplayName,
                Email = AddedUser.Email,
                Token =await token.CreateToken(AddedUser, userManager)
            };
            return Ok(ReturnedUser);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDtocs>> Login(LoginUserDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user == null) { return Unauthorized(new APIResponse(401)); }
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password,false);
            if (!result.Succeeded) { return Unauthorized(new APIResponse(401)); }
            var ReturnedUser = new UserDtocs()
            {
                DiaplyName = user.DisplayName,
                Email = user.Email,
                Token = await token.CreateToken(user, userManager)
            };
            return Ok(ReturnedUser);
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            var result = await userManager.FindByEmailAsync(email);
            if (result == null) { return Ok(false); }
            return Ok(true);
        }
        [Authorize]
        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<UserDtocs>> GetCurrentUser()
        {
            var user = await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            if (user == null) { return Unauthorized(new APIResponse(401)); }
            var ReturnedUser = new UserDtocs()
            {
                DiaplyName = user.DisplayName,
                Email = user.Email,
                Token = await token.CreateToken(user, userManager)
            };
            return Ok(ReturnedUser);
        }
        [Authorize]
        [HttpGet("getcurrentuseraddress")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
           
            var user = await userManager.GetAddress(User);
            if (user == null) { return NotFound(new APIResponse(400,"UnAuthorized")); }
            var address = new AddressDto()
            {
                firstname = user.address.firstname,
                lastname = user.address.lastname,
                street = user.address.street,
                country = user.address.country,
                city = user.address.city,
               
            };
            if(address == null) { return NotFound(new APIResponse(404,"User dosen't have address")); }
            return Ok(address);
        }
        [Authorize]
        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {
            var user = await userManager.GetAddress(User);
            if (user == null) { return NotFound(new APIResponse(400, "UnAuthorized")); }
            user.address.firstname = address.firstname;
            user.address.lastname = address.lastname;
            user.address.street = address.street;
            user.address.country = address.country;
            user.address.city = address.city;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) { return BadRequest(new APIResponse(400)); }
            return Ok(address);
        }

    }
}
