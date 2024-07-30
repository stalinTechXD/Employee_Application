using EmployeeApplication.Models;
using EmployeeApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EmployeeApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(EmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AddEmployee(bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeRepository.AddNewEmp(employee);
                if (result > 0)
                {
                    return RedirectToAction(nameof(AddEmployee), new { isSuccess = true });
                }

                _logger.LogWarning("Failed to add new employee.");
                return View();
            }

            LogModelStateErrors();
            return View(employee);
        }

        public async Task<ViewResult> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployees();
            return View(employees);
        }

        public async Task<ViewResult> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployee(id);
            return View(employee);
        }

        private void LogModelStateErrors()
        {
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }
        }
    }
}
