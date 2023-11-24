using ADOTest.Models;
using System.Data.SqlClient;

namespace ADOTest.Repositories
{
    public class EmployeeRepository : IEmloyeeRepository
    {
        private readonly IConfiguration _configuration;
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Employee> allEmployee()
        {
            
               List<Employee> allEmp = new List<Employee>();

             string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
             SqlConnection connection = new SqlConnection(connectionString);
             string query = "empById";
             SqlCommand cmd = new SqlCommand(query, connection);
             cmd.CommandType = System.Data.CommandType.StoredProcedure;
             connection.Open();
             using (SqlDataReader reader = cmd.ExecuteReader())
             {
                 while (reader.Read())
                 {
                     Employee emp = new Employee();


                     emp.id = Convert.ToInt32(reader["id"]);
                     emp.name = Convert.ToString(reader["Name"]);
                     emp.age = Convert.ToInt32(reader["AGE"]);
                     emp.salary = Convert.ToInt32(reader["SALARY"]);
                     emp.deptId = Convert.ToInt32(reader["deptId"]);
                     emp.city = Convert.ToString(reader["city"]);
                     allEmp.Add(emp);
                 }
             }
             connection.Close();
            return allEmp;
            
        }

        public void deleteEmployee(int? id)
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "deleteEmployee";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void EditEmployee(Employee employee)
        {
            
        }

        public IEnumerable<Employee> searchEmployee(dynamic searchTerm)
        {
            List<Employee> allAlb = new List<Employee>();

            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "searchEmps";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empName",searchTerm);
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Employee emp = new Employee();


                    emp.id = Convert.ToInt32(reader["id"]);
                    emp.name = Convert.ToString(reader["name"]);
                    emp.age = Convert.ToInt32(reader["age"]);
                    emp.salary = Convert.ToInt32(reader["salary"]);
                    emp.deptId = Convert.ToInt32(reader["deptId"]);
                    emp.city = Convert.ToString(reader["City"]);
                    allAlb.Add(emp);
                }
            }
            connection.Close();
            return allAlb;
        }

       
    }
}
