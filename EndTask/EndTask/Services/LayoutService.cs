using EndTask.DAL;

namespace EndTask.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetLayout()
        {
           return _context.Settings.ToList().ToDictionary(x=>x.Key, x => x.Value);
        }
    }
}
