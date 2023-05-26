using Task.Models;

namespace Task.ViewModels
{
    public class HomeVM
    {
        public List<Blog> blogs { get; set; }
        public Slider slider { get; set; }
        public List<Team> Teams{ get; set; }
        public List <OurServices> OurServices { get; set; }
    }
}
