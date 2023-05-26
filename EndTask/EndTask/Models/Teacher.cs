namespace EndTask.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Job { get; set; }
        public List<Courses> Courses { get; set; }
    }
}
