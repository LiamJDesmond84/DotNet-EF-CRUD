using Microsoft.EntityFrameworkCore;

namespace DotNet_EF_CRUD.Models
{
    public class MyContext : DbContext
    {

        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<Dish> Dishes { get; set; }
    }
}
