﻿using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Interfaces.Services
{
	public interface ITokenService
	{
		Task<string> CreateToken(AppUser user);
	}
}
