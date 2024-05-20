using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.ApiResponse;
using Talabat.API.Dtos;
using Talabat.Core.Enities;

namespace Talabat.API.Controllers
{

    public class Account : BController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public Account(UserManager<AppUser> UserManager , SignInManager<AppUser> SignInManager)
        {
            _userManager = UserManager;
            _signInManager = SignInManager;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> UserLogin(LoginDto Model){

          var User = await _userManager.FindByEmailAsync(Model.Email);

            if (User is null)
                return Unauthorized(new ObjResponse(401, "This Email Not Found"));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, Model.Password , false);

            if (Result.Succeeded is false)
                return Unauthorized(new ObjResponse(401, "Invalid Password"));


            var Data = new UserDto()
            {
                Response = new { Status = "200", Message = "User Login Successfully" },
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = "This Your Token",
            };

            return Ok(Data);



        }



        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> UserRegister(RegisterDto Model)
        {

            var User = new AppUser()
            {
                DisplayName = Model.DisplayName,
                Email = Model.Email,
                UserName = Model.Email.Split("@")[0],
                PhoneNumber = Model.PhoneNumber,
            };


            var Result = await _userManager.CreateAsync(User, Model.Password);
            if (Result.Succeeded is false)
                return Unauthorized(new ObjResponse(401, "Error In Creating The User"));



            var Data = new UserDto()
            {
                Response = new { Status = "200", Message = "User Created Successfully" },
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = "This Your Token",
            };

            return Ok(Data);


        }


    }
}
