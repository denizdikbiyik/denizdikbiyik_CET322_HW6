using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using denizdikbiyik_CET322_HW5.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using denizdikbiyik_CET322_HW5.Data;
using Microsoft.AspNetCore.Authorization;

namespace denizdikbiyik_CET322_HW5.Controllers
{
    [Authorize(Roles = "admin, departmentadmin")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StudentsController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var studentsContext = _context.Student.Include(s => s.Department);
            return View(await _context.Student.ToListAsync());
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.Include(s => s.Department).FirstOrDefaultAsync(m => m.StudentId == id);
            var kaydeden = _context.Users.FirstOrDefaultAsync(u => u.Id == student.CetUserId);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<Department> departmentlist = new List<Department>();
            departmentlist = (from Department in _context.Department select Department).ToList();
            Student student = new Student();
            student.departments = GetDepartments(departmentlist);
            return View(student);
        }
        private IEnumerable<SelectListItem> GetDepartments(IEnumerable<Department> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.DepartmentName
                });
            }

            return selectList;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, Student studentmodel, IFormFile FileUrl)
        {
            studentmodel.CreatedDate = DateTime.Now;

            var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            studentmodel.CetUserId = loginUser?.Id;

            if (ModelState.IsValid)
            {
                string dirPath = Path.Combine(_hostingEnvironment.WebRootPath, @"uploads\");
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + FileUrl.FileName;
                using (var fileStream = new FileStream(dirPath + fileName, FileMode.Create))
                {
                    await FileUrl.CopyToAsync(fileStream);
                }             

                Student student = new Student();
                student.StudentNo = studentmodel.StudentNo;
                student.StudentName = studentmodel.StudentName;
                student.StudentSurname = studentmodel.StudentSurname;
                student.StudentEmail = studentmodel.StudentEmail;
                student.StudentTelNo = studentmodel.StudentTelNo;
                student.ImageUrl = fileName;
                student.Content = studentmodel.Content;
                student.CreatedDate = studentmodel.CreatedDate;
                student.DepartmentId = studentmodel.DepartmentId;
                student.CetUserId = studentmodel.CetUserId;

                _context.Student.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentmodel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            List<Department> departmentlist = new List<Department>();
            departmentlist = _context.Department.ToList();
            student.departments = GetDepartments(departmentlist);

            var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            if (student.CetUserId != loginUser.Id)
            {
                return Unauthorized();
            }           

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentNo,StudentName,StudentSurname,StudentEmail,StudentTelNo,DepartmentId,ImageUrl,Content,CreatedDate, CetUserId")] Student student, IFormFile FileUrl)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var existingstudent = await _context.Student.Include(p => p.CetUser).FirstOrDefaultAsync(p => p.StudentId == student.StudentId);
                    if (!(existingstudent.CetUser?.UserName == User.Identity.Name || User.IsInRole("admin")))
                    {
                        return Unauthorized();

                    }

                    if (FileUrl != null)
                    {
                        string dirPath = Path.Combine(_hostingEnvironment.WebRootPath, @"uploads\");
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + FileUrl.FileName;
                        using (var fileStream = new FileStream(dirPath + fileName, FileMode.Create))
                        {
                            await FileUrl.CopyToAsync(fileStream);
                        }
                        existingstudent.ImageUrl = fileName;
                    }

                    existingstudent.CreatedDate = DateTime.Now;

                    _context.Student.Update(existingstudent);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.Include(s => s.Department).FirstOrDefaultAsync(m => m.StudentId == id);
            var kaydeden = _context.Users.FirstOrDefaultAsync(u => u.Id == student.CetUserId);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}
