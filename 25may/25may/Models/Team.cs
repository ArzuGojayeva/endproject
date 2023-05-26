using System.ComponentModel.DataAnnotations.Schema;

namespace _25may.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ProfessionId { get; set; }
        public Profession? Profession { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
