using ADOTest.Models;

namespace ADOTest.ViewModels
{
    public class IndexSearch
    {
        public int searchTerm { get; set; }
        public IEnumerable<Employee> employees { get; set; }
    }
}
