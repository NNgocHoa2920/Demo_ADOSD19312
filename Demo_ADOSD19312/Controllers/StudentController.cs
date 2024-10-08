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

        //đi tới trang edit với dữ liệu cần edit
        public IActionResult Edit(int id)
        {
            //lấy râ thông tin của student cần xóa
            var stEdit = _studentDAL.GetStudentByID(id);
            return View(stEdit);
        }
        [HttpPost]
        public IActionResult Edit(Student st)
        {
            _studentDAL.UpdateSudent(st);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id) 
        { 
            _studentDAL.DeleteST(id);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var stde = _studentDAL.GetStudentByID(id);
            return View(stde);
        }

    }
}
