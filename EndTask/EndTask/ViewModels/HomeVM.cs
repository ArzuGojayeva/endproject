using EndTask.Models;

namespace EndTask.ViewModels
{
    public class HomeVM
    {
        public List<OurServices>Services { get; set; }
        public List<Courses>Courses { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
