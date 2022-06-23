using System;
using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Data.Dto.Users;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Repository
{
	public class AuthManager : IAuthManager
	{
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager; //this  is the library we added for user that handles everything and has a lot of commands to help you out, if it doesnt have something you will have to extend it

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
		{
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            var user = _mapper.Map<ApiUser>(userDto);
            user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user, userDto.Password)

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");//user gets added to User Role.
            }

            return result.Errors;
        }
    }
}

