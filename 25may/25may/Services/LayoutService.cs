using _25may.DAL;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace _25may.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetLayouts() {
           return _context.Settings.ToList().ToDictionary(x=>x.Key, x => x.Value);
        }
    }
}
