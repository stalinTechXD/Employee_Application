using EmployeeApplication.Models;
using EmployeeApplication.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(EmployeeRepository employeeRepo, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult AddEmployee(bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }

        public async Task<ViewResult> GetEmployees()
        {

            var data = await _employeeRepository.GetEmployees();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                
                var result = await _employeeRepository.AddNewEmp(employee);
                if (result > 0)
                    return RedirectToAction(nameof(AddEmployee), new { isSuccess = true });
                return View();
            }

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }

            return View(employee);
        }

        public async Task<ViewResult> GetEmployee(int id)
        {

            var ans = await _employeeRepository.GetEmployee(id);

            return View(ans);
        }

    }
}
