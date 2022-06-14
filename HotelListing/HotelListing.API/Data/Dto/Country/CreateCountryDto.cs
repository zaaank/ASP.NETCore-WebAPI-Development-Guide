using System;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Data.Dto.Country
{
	public class CreateCountryDto
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}

