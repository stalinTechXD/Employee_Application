using System.Collections.Specialized;
using EmployeeApplication.Data;
using EmployeeApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApplication.Repository
{
    public class EmployeeRepository
    {
        private readonly EmployeeContext _employeeContext = null;

        public EmployeeRepository(EmployeeContext employeecontext)
        {
            _employeeContext = employeecontext;
        }
        public async Task<int> AddNewEmp(EmployeeModel model)
        {
            var newEmployee = new Employees();
            //newEmployee.Id = model.Id;
            newEmployee.Address = model.Address;
            newEmployee.Name = model.Name;
            newEmployee.Designation = model.Designation;
            newEmployee.Salary = model.Salary;
            newEmployee.Description = model.Description;

            // make a entry inside the database.
            await _employeeContext.Employees.AddAsync(newEmployee);
            await _employeeContext.SaveChangesAsync();

            return newEmployee.Id;
        }
        public async Task<List<EmployeeModel>> GetEmployees()
        {
            var emps = new List<EmployeeModel>();
            var employees = await _employeeContext.Employees.ToListAsync();
            if (employees?.Any() == true)
            {
                foreach (var emp in employees)
                {
                    emps.Add(new EmployeeModel()
                    {
                        Id = emp.Id,
                        Name = emp.Name,
                        Designation = emp.Designation,
                        Address = emp.Address,
                        Salary = emp.Salary
                    });
                }
            }
            return emps;
        }

        public async Task<List<EmployeeModel>> GetTop3Employees()
        {
            var emps = new List<EmployeeModel>();
            var employees = await _employeeContext.Employees.Take(3).ToListAsync();
            if (employees?.Any() == true)
            {
                foreach (var emp in employees)
                {
                    emps.Add(new EmployeeModel()
                    {
                        Id = emp.Id,
                        Name = emp.Name,
                        Designation = emp.Designation,
                        Address = emp.Address,
                        Salary = emp.Salary
                    });
                }
            }
            return emps;
        }   
        public async Task<EmployeeModel> GetEmployee(int id)
        {
            var emp = new EmployeeModel();

            var employee = await _employeeContext.Employees.FindAsync(id);
            if (employee != null)
            {
                return new EmployeeModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Designation = employee.Designation,
                    Address = employee.Address,
                    Salary = employee.Salary
                };
            }

            return null;
        }

        public List<EmployeeModel> searchEmployee(string name, string address)
        {
            return DataSource()?.Where(x => x?.Name == name || x?.Address == address).ToList();
        }
        private List<EmployeeModel> DataSource()
        {
            return new List<EmployeeModel>() {
                new EmployeeModel() { Id = 1 , Address = "usa", Name ="stalin" , Designation = "senior dev"},
                new EmployeeModel() { Id = 2 , Address = "india", Name ="sunny" ,Designation ="junior"},
                new EmployeeModel() { Id = 3 , Address = "france", Name ="leena" , Designation = "ceo"}
            };
        }

    }
}
