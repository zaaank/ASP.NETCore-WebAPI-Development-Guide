using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Data.Dto.Country;
using AutoMapper;
using HotelListing.API.Configurations;
using HotelListing.API.Contracts;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController( IMapper mapper, ICountriesRepository countriesRepository)
        {
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countriesRepository.GetAllAsync();
            var mapped = _mapper.Map<List<GetCountryDto>>(countries); //now mapped contains only the properties of getcountry dto, without any other fields.

            return Ok(mapped);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDetailsDto>> GetCountry(int id)
        {
            
            var country = await _countriesRepository.GetCountryWithHotelListAsync(id);

            if (country == null)
            {
                return NotFound("Country was not found");
            }

            var countryDetailsDto = _mapper.Map<CountryDetailsDto>(country);

            return Ok(countryDetailsDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("Ids doesn't match");
            }

            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound("Country does not exist in database");
            }

                _mapper.Map(updateCountryDto, country); //it takes everything from the left object and map it into the right, so country now also contains all information from updatecountry dto

            try
            {
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CountryExists(id))
                {
                    return NotFound("Couldn't save the changes");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {
            //var oldCountry = new Country
            //{
                //Name = createCountryDto.Name,
                //ShortName = createCountryDto.ShortName
            //};
            var country = _mapper.Map<Country>(createCountryDto); //this will autoMap this field like it says

            var added =  await _countriesRepository.AddAsync(country);
          if (added == null)
          {
              return Problem("Entity set 'HotelListingDbContext.Countries'  is null.");
          }

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);
            if(country == null)
            {
                return NotFound("Country you are trying to delete does not exists");
            }
            await _countriesRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
