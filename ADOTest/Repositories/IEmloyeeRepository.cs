using ADOTest.Models;

namespace ADOTest.Repositories
{
    public interface IEmloyeeRepository
    {
        void deleteEmployee(int? id);
        IEnumerable<Employee> searchEmployee(dynamic searchTerm);
        IEnumerable<Employee> allEmployee();
    }
}
