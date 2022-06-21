using System;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelListingDbContext _context;

        public CountriesRepository(HotelListingDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Country> GetCountryWithHotelListAsync(int? id)
        {
            if(_context.Countries is null)
            {
                return null;
            }
            if(id == null)
            {
                return null;
            }
            return await _context.Countries.Include(q => q.Hotels).FirstOrDefaultAsync(q => q.Id == id); //it contexta vzame contries, includa Hotel, ki je v entities definirana relacija in potem uzame prvo državo ki ima id enak id ki je vpisan, 
        }
    }
}

