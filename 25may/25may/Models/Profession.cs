namespace _25may.Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public List<Team> Team { get; set; }
    }
}
