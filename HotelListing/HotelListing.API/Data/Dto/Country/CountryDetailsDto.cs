using System;
using HotelListing.API.Configurations;

namespace HotelListing.API.Data.Dto.Country
{
	public class CountryDetailsDto : BaseCountryDto
	{
			public int Id { get; set; }

			public List<GetHotelDto> Hotels { get; set; }
		
	}
}

