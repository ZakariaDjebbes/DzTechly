using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.Identity;
using API.Errors;
using AutoMapper;
using Core.Entities.Identity;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdministrationController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;

        public AdministrationController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet("users")]
        [Authorize(Policy = "RequireAdministration")]
        public async Task<ActionResult<Pagination<UserForAdministrationDto>>> GetUsers([Required][FromQuery] UsersForAdministrationParams specParams)
        {
            var count = await _userManager.Users.CountAsync();
            var users = await _userManager.Users
            .OrderBy(x => x.UserName)
            .Skip(specParams.PageSize * (specParams.PageIndex - 1))
            .Take(specParams.PageSize)
            .Include(x => x.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();
            var mappedUsers = _mapper.Map<IReadOnlyList<AppUser>, IReadOnlyList<UserForAdministrationDto>>(users);
            return Ok(new Pagination<UserForAdministrationDto>(specParams.PageIndex, specParams.PageSize, count, mappedUsers));
        }

        [HttpGet("roles")]
        [Authorize(Policy = "RequireAdministration")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetRoles()
        {
            return Ok(await _roleManager.Roles.Select(x => x.Name).ToListAsync());
        }

        [HttpDelete("deleteUser")]
        [Authorize(Policy = "RequireAdministration")]
        public async Task<ActionResult> DeleteUser([FromQuery][Required] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest(new ApiResponse(400, "This user doesn't exist"));
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            return NoContent();
        }

        [HttpPut("updateRoles")]
        [Authorize(Policy = "RequireAdministration")]
        public async Task<ActionResult> UpdateUserRoles(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(updateUserDto.Id);

            if (user == null)
                return BadRequest(new ApiResponse(400, "This user doesn't exist"));

            await _userManager.RemoveFromRolesAsync(user, _roleManager.Roles.Select(x => x.Name).ToArray());
            var result = await _userManager.AddToRolesAsync(user, updateUserDto.Roles);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            return NoContent();
        }
    }
}