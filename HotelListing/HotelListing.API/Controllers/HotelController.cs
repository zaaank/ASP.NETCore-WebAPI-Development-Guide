using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Contracts;
using HotelListing.API.Configurations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelsRepository _hotelRepo;

        public HotelController(IMapper mapper, IHotelsRepository hotelRepo)
        {
            this._mapper = mapper;
            this._hotelRepo = hotelRepo;
        }

        // GET: api/Hotel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
        {
            var hotels = await this._hotelRepo.GetAllAsync();
            var mapped = this._mapper.Map<List<GetHotelDto>>(hotels);
            return mapped;
        }

        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
        {
            var hotel = await this._hotelRepo.GetAsync(id);
          if (hotel == null)
          {
              return NotFound("Hotel not found");
          }

            return Ok(this._mapper.Map<GetHotelDto>(hotel));
        }

        // PUT: api/Hotel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDto hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest("Ids Doesn't match");
            }

            var hotel = await this._hotelRepo.GetAsync(id);
            if(hotel == null)
            {
                return NotFound("Hotel not found");
            }
           

            try
            {
                await this._hotelRepo.UpdateAsync(this._mapper.Map(hotelDto, hotel));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await HotelExists(id))
                {
                    return NotFound("hotel not found");
                }
                else
                {
                    return NotFound("Error unknown - Zan");
                }
            }

            return NoContent();
        }

        // POST: api/Hotel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto createHotelDto)
        {
            var mappedHotel = this._mapper.Map(createHotelDto, new Hotel());
            var postedHotel = await this._hotelRepo.AddAsync(mappedHotel);
          if (postedHotel == null)
          {
              return Problem("Entity set 'HotelListingDbContext.Hotels'  is null.");
          }

            return CreatedAtAction("GetHotel", new { id = postedHotel.Id }, postedHotel);
        }

        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await this._hotelRepo.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            await this._hotelRepo.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await this._hotelRepo.Exists(id);
        }
    }
}
