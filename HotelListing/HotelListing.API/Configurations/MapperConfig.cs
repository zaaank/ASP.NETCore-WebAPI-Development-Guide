using System;
using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.Data.Dto.Country;

namespace HotelListing.API.Configurations
{
	public class MapperConfig : Profile
	{
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap(); //Reverse map allow us to also map from dto to country
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDetailsDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();


            CreateMap<Hotel, GetHotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();

        }
    }
}

