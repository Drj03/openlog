using ADOTest.Models;
using ADOTest.Repositories;
using ADOTest.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ADOTest.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmloyeeRepository repo;
        public HomeController(IEmloyeeRepository repository, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            repo = repository;
        }
        [Route("")]
        [Route("~/Home")]
        [Route("~/")]
       

       // [Route("{search}")]
       // [HttpPost]
        public IActionResult Index(string search)
        {
            ViewBag.Search = search; 
            var allEmloyee = repo.allEmployee();
            if (string.IsNullOrEmpty(search))
            {
                
                return View(allEmloyee);
            }


            var model = repo.searchEmployee(search);
            return View(model);
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "employeeById";
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter parameter = new SqlParameter("@id", id);
            command.Parameters.Add(parameter);
            connection.Open();
            Employee emp = new Employee();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {


                    emp.id = Convert.ToInt32(reader["id"]);
                    emp.name = Convert.ToString(reader["Name"]);
                    emp.age = Convert.ToInt32(reader["AGE"]);
                    emp.salary = Convert.ToInt32(reader["SALARY"]);
                    emp.deptId = Convert.ToInt32(reader["deptId"]);
                    emp.city = Convert.ToString(reader["city"]);
                    //allEmp.Add(emp);

                }
            }
            connection.Close();
            return View(emp);
        }

        public IActionResult EmployeeWithDepartment()
        {
            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];

            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select name,age,salary,deptName,cmpName\r\nfrom employee\r\ninner join depts\r\non employee.deptId=depts.id\r\ninner join compony\r\non\r\ndepts.cmpId=compony.cmpId;";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            List<EmployeesDepartmentViewModel> empList = new List<EmployeesDepartmentViewModel>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    EmployeesDepartmentViewModel emp = new EmployeesDepartmentViewModel();


                    //  emp.id = Convert.ToInt32(reader["id"]);
                    emp.name = Convert.ToString(reader["Name"]);
                    emp.age = Convert.ToString(reader["AGE"]);
                    emp.salary = Convert.ToInt32(reader["SALARY"]);
                    //   emp.deptId = Convert.ToInt32(reader["deptId"]);
                    //   emp.city = Convert.ToString(reader["city"]);
                    emp.deptName = Convert.ToString(reader["deptName"]);
                    emp.componyName = Convert.ToString(reader["cmpName"]);
                    empList.Add(emp);
                }
            }
            connection.Close();
            return View(empList);
        }
        
        public IActionResult EmpwithDept()
        {
           List<Employee> allEmp = new List<Employee>
        ();

            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "select *from employee";
            SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Employee e = new Employee();
                    e.id = Convert.ToInt32(reader["ID"]);
                    e.name = Convert.ToString(reader["name"]);
                    e.age = Convert.ToInt32(reader["age"]);
                    e.salary = Convert.ToInt32(reader["salary"]);
                    e.deptId = Convert.ToInt32(reader["deptId"]);
                    e.city = Convert.ToString(reader["city"]);
                    allEmp.Add(e);
                }
            }
            connection.Close();

         

            EmpandDeptViewModel empv = new EmpandDeptViewModel()
            {
                emp = allEmp,
            };

            connection.Close();
            return View(empv);
        }

       [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        

        [HttpPost]
        public IActionResult CreateEmployee(Employee emp)
        {

            string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "createEmp";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@id", emp.id);
            cmd.Parameters.AddWithValue("@name", emp.name);
            cmd.Parameters.AddWithValue("@age", emp.age);
            cmd.Parameters.AddWithValue("@salary", emp.salary);
            cmd.Parameters.AddWithValue("@deptId", emp.deptId);
            cmd.Parameters.AddWithValue("@city", emp.city);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index");
        }
        [Route("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
          
            repo.deleteEmployee(id);
            return RedirectToAction("Index");
        }

        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Test(string firstName,string lastName,int rollNum)
        {
            string name = string.Format("Name: {0} {1} {2}", firstName, lastName,rollNum); ;
            return Json(new { Status = "success", Name = name });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}