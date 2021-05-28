using System.Globalization;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using Core.Interfaces.Services;
using API.Dtos.Identity;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signIn;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSender;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signIn,
            ITokenService tokenService,
            IMapper mapper,
            IEmailSenderService emailSender)
        {
            _userManager = userManager;
            _signIn = signIn;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailSender = emailSender;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByClaimsAsync(HttpContext.User);

            return new UserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExist([Required][FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("userExists")]
        public async Task<ActionResult<bool>> CheckUserExist([FromQuery][Required] string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindByClaimsWithAddressAndInfoAsync(HttpContext.User);

            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        [HttpGet("info")]
        [Authorize]
        public async Task<ActionResult<PersonalInformationDto>> GetUserInfos()
        {
            var user = await _userManager.FindByClaimsWithAddressAndInfoAsync(HttpContext.User);

            return _mapper.Map<PersonalInformation, PersonalInformationDto>(user.PersonalInformation);
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signIn.CheckPasswordSignInAsync(user, loginDto.Password, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    return Unauthorized(new ApiResponse(401, "Too many failed attemps to login, please try again later"));
                else if (result.IsNotAllowed)
                {
                    return Unauthorized(new ApiResponse(401, "Your email address isn't verfied"));
                }
                else
                    return Unauthorized(new ApiResponse(401));
            }

            return new UserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Address = _mapper.Map<AddressDto, Address>(registerDto.Address),
                PersonalInformation = _mapper.Map<PersonalInformationDto, PersonalInformation>(registerDto.PersonalInformation)
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            result = await _userManager.AddToRoleAsync(user, "Client");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

            await _emailSender.SendConfirmationEmailAsync(user.Email, encodedToken, user.UserName);
            return Ok(true);
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {
            var user = await _userManager.FindByClaimsWithAddressAndInfoAsync(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(address);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400, "Problem updating the user address"));
            else
                return Ok(address);
        }

        [HttpPut("info")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateInfo(PersonalInformationDto personalInformationDto)
        {
            var user = await _userManager.FindByClaimsWithAddressAndInfoAsync(HttpContext.User);
            var info = _mapper.Map<PersonalInformationDto, PersonalInformation>(personalInformationDto);
            user.PersonalInformation.BirthDate = info.BirthDate;
            user.PersonalInformation.FirstName = info.FirstName;
            user.PersonalInformation.LastName = info.LastName;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400, "Problem updating the user informations"));
            else
                return Ok(personalInformationDto);
        }


        [HttpPut("updatePassword")]
        [Authorize]
        public async Task<ActionResult<bool>> UpdatePassword(UpdatePasswordDto updatePasswordDto)
        {
            var user = await _userManager.FindByClaimsWithAddressAndInfoAsync(HttpContext.User);
            var result = await _userManager.ChangePasswordAsync(user, updatePasswordDto.OldPassword, updatePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            return Ok(true);
        }

        [HttpPut("updateProfile")]
        [Authorize]
        public async Task<ActionResult<UserDto>> UpdateUser(RegisterDto registerDto)
        {
            var user = await _userManager.FindByClaimsWithAddressAndInfoAsync(HttpContext.User);
            var signIn = await _signIn.CheckPasswordSignInAsync(user, registerDto.Password, false);

            if (!signIn.Succeeded)
                return Unauthorized(new ApiResponse(401));

            user.UserName = registerDto.UserName;
            user.Email = registerDto.Email;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new UserDto
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = await _tokenService.CreateToken(user)
                };
            }

            return new BadRequestObjectResult(new ApiValidationErrorResponse
            {
                Errors = result.Errors.Select(x => x.Description)
            });
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] [Required] [EmailAddress] string email, [FromQuery] [Required] string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest(new ApiResponse(400, "This email address doesn't exist"));

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            var resultToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, resultToken);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            return Ok(true);
        }

        [HttpGet("resetPassword")]
        public async Task<IActionResult> PasswordReset([FromQuery] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest(new ApiResponse(400, "User not found"));

            var decodedToken = WebEncoders.Base64UrlDecode(resetPasswordDto.Token);
            var resultToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ResetPasswordAsync(user, resultToken, resetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            return Ok(true);
        }

        [HttpGet("requestConfirmationEmail")] 
        public async Task<IActionResult> RequestEmailConfirmation([FromQuery] [Required] [EmailAddress] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
                return BadRequest(new ApiResponse(400, "This email address doesn't exist"));

            if (user.EmailConfirmed)
                return BadRequest(new ApiResponse(400, "This email address is already confirmed"));

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

            await _emailSender.SendConfirmationEmailAsync(user.Email, encodedToken, user.UserName);

            return Ok();
        }

        [HttpGet("requestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset([FromQuery] [Required] [EmailAddress] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return BadRequest(new ApiResponse(400, "This email does not exist"));
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            var encodedToken = WebEncoders.Base64UrlEncode(tokenBytes);

            await _emailSender.SendPasswordResetEmailAsync(user.Email, encodedToken, user.UserName);

            return Ok();
        }
    }
}