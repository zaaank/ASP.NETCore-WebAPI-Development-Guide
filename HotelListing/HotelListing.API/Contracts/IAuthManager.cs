using System;
using HotelListing.API.Data.Dto.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
	}
}

