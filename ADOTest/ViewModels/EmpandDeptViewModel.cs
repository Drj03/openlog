using ADOTest.Models;

namespace ADOTest.ViewModels
{
    public class EmpandDeptViewModel
    {
        public IEnumerable<Employee> emp { get; set; }
        public IEnumerable<Department> depts { get; set; }
        public IEnumerable<Compony> comp { get; set; }
    }
}
