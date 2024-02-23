using MagicVilla.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Villa 1",
                    Details = "Villa 1 Details",
                    Rate = 100,
                    Sqft = 1000,
                    Occupancy = 4,
                    ImageUrl = "https://via.placeholder.com/150",
                    Amenity = "Villa 1 Amenity",
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                },
                new Villa
                {
                    Id = 2,
                    Name = "Villa 2",
                    Details = "Villa 2 Details",
                    Rate = 200,
                    Sqft = 2000,
                    Occupancy = 6,
                    ImageUrl = "https://via.placeholder.com/150",
                    Amenity = "Villa 2 Amenity",
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                },
                new Villa
                {
                    Id = 3,
                    Name = "Villa 3",
                    Details = "Villa 3 Details",
                    Rate = 300,
                    Sqft = 3000,
                    Occupancy = 8,
                    ImageUrl = "https://via.placeholder.com/150",
                    Amenity = "Villa 3 Amenity",
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                },
                new Villa
                {
                    Id = 4,
                    Name = "Villa 4",
                    Details = "Villa 4 Details",
                    Rate = 400,
                    Sqft = 4000,
                    Occupancy = 10,
                    ImageUrl = "https://via.placeholder.com/150",
                    Amenity = "Villa 4 Amenity",
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                },
                new Villa
                {
                    Id = 5,
                    Name = "Villa 5",
                    Details = "Villa 5 Details",
                    Rate = 500,
                    Sqft = 5000,
                    Occupancy = 12,
                    ImageUrl = "https://via.placeholder.com/150",
                    Amenity = "Villa 5 Amenity",
                    CreatedAt = System.DateTime.Now,
                    UpdatedAt = System.DateTime.Now,
                }
            );
        }
    }
}
