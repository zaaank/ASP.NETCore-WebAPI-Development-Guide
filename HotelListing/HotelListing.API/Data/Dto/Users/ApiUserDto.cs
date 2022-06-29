using System;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Data.Dto.Users
{
    public class ApiUserDto :LoginDto
	{
		public ApiUserDto()
		{
		}
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }

}

