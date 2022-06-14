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

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public CountriesController(HotelListingDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
          if (_context.Countries == null)
          {
              return NotFound();
          }
            var countries =  await _context.Countries.ToListAsync();
            var mapped = _mapper.Map<List<GetCountryDto>>(countries);

            return Ok(mapped);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDetailsDto>> GetCountry(int id)
        {
          if (_context.Countries == null)
          {
              return NotFound();
          }
            // var country = await _context.Countries.FindAsync(id);

            //var country = await _context.Countries.Include(q => q.Hotels).FirstOrDefault(q => q.Id == id);
            var country = await _context.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.Id == id); //it contexta vzame contries, includa Hotel, ki je v entities definirana relacija in potem uzame prvo državo ki ima id enak id ki je vpisan, 

            if (country == null)
            {
                return NotFound();
            }

            var countryDetailsDto = _mapper.Map<CountryDetailsDto>(country);

            return Ok(countryDetailsDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
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
            var oldCountry = new Country
            {
                Name = createCountryDto.Name,
                ShortName = createCountryDto.ShortName
            };
            var country = _mapper.Map<Country>(createCountryDto); //this will autoMap this field like it says
          if (_context.Countries == null)
          {
              return Problem("Entity set 'HotelListingDbContext.Countries'  is null.");
          }
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
