using System;
using HotelListing.API.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
	public class HotelListingDbContext : IdentityDbContext<ApiUser>
	{
        public HotelListingDbContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration()); //in this files we can set properties like different table names, seed data etc...
            modelBuilder.ApplyConfiguration(new HotelConfiguration());

            modelBuilder.ApplyConfiguration(new CountryConfiguration());
        }
    }
}

