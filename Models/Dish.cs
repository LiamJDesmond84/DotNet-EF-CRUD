using System.ComponentModel.DataAnnotations;

namespace DotNet_EF_CRUD.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        public String Name { get; set; }

        public String Chef { get; set; }

        public int Tastiness { get; set; }

        public int Calories { get; set; }

        public String Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set;} = DateTime.Now;
    }
}
