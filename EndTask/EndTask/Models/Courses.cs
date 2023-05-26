using System.ComponentModel.DataAnnotations.Schema;

namespace EndTask.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public string? Name { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
