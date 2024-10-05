using Demo_ADOSD19312.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_ADOSD19312.Controllers
{
    public class StudentController : Controller
    {
        StudentDAL _studentDAL;
        public StudentController(StudentDAL studentDAL)
        {
            _studentDAL = studentDAL;
        }
        public IActionResult Index()
        {
            var lstStudent = _studentDAL.GetAllStudent();
            return View(lstStudent);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            _studentDAL.AddStudent(student);
            return RedirectToAction("Index");
        }
    }
}
