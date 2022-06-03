﻿using System;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
	public class HotelListingDbContext : DbContext
	{
        public HotelListingDbContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> countries { get; set; }
	}
}

