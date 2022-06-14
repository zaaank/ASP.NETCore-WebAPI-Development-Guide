using System;
using HotelListing.API.Configurations;

namespace HotelListing.API.Data.Dto.Country
{
	public class CountryDetailsDto
	{
			public int Id { get; set; }
			public string Name { get; set; }
			public string ShortName { get; set; }

			public List<GetHotelDto> Hotels { get; set; }
		
	}
}

