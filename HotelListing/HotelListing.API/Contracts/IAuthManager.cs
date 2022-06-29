using System;
using HotelListing.API.Data.Dto.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto, bool isAdmin = false);
		Task<AuthResponseDto> Login(LoginDto loginDto);
		Task<string> CreateRefreshToken();
		Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
	}
}

