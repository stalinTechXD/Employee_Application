using EmployeeApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApplication.Controllers
{
    public class HomeController:Controller
    {
        private readonly EmployeeRepository _employeeRepository;


        public HomeController(EmployeeRepository employeeRepo)
        {
            _employeeRepository = employeeRepo;
        }
        public async Task< ViewResult> Index() {
            var data = await _employeeRepository.GetTop3Employees();
            return View(data );
        }

        public ViewResult AboutUs() {
            return View();
        }

        public ViewResult ContactUs() {
            return View();
        }
    }
}
