using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmpDbContext _context;

        public EmployeeRepository(EmpDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool InsertEmployee(Employee employee)
        {
            var result = false;
            if (employee != null)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                result = true;
            }

            return result;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            employees = _context.Employees.Include(p => p.Department).ToList();
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = _context.Employees.Include(p => p.Department).FirstOrDefault(c => c.EmployeeId == id);
            return employee;
        }

        public bool UpdateEmployee(Employee employee)
        {
            var result = false;
            if (employee != null)
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                result = true;
            }

            return result;
        }
        
        public bool DeleteEmployee(int id)
        {
            var result = false;
            if (id > 0)
            {
                var employee = _context.Employees.Find(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                    result = true;
                }
            }

            return result;
        }

        public int TotalEmployee()
        {
            return _context.Employees.Count();
        }

        public IEnumerable<Employee> GetPagintaedEmployee(int page, int pagesize)
        {
            int skip =(page-1) *pagesize;
            return _context.Employees.Include(p => p.Department).ToList()
                .OrderBy(e => e.EmployeeId)
                .Skip(skip)
                .Take(pagesize);
        }
    }
}
