using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using denizdikbiyik_CET322_HW5.Models;
using denizdikbiyik_CET322_HW5.Data;
using Microsoft.AspNetCore.Authorization;

namespace denizdikbiyik_CET322_HW5.Controllers
{
    [Authorize(Roles = "admin, departmentadmin")]
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Department.ToListAsync());
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.Include(d => d.Students).FirstOrDefaultAsync(m => m.Id == id);
            var kaydeden = _context.Users.FirstOrDefaultAsync(u => u.Id == department.CetUserId);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentName")] Department department)
        {
            department.CreatedDate = DateTime.Now;
            var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            department.CetUserId = loginUser?.Id;

            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null) 
            {
                return NotFound();
            }

            var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            if(department.CetUserId!=loginUser.Id) {
                return Unauthorized();
            }
            return View(department);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var currentDepartment = await _context.Department.Include(p => p.CetUser).FirstOrDefaultAsync(p => p.Id == department.Id);
                    if (!(currentDepartment.CetUser?.UserName == User.Identity.Name || User.IsInRole("admin")))
                    {
                        return Unauthorized();

                    }

                    currentDepartment.CreatedDate = DateTime.Now;
                    _context.Update(currentDepartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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
            return View(department);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FirstOrDefaultAsync(m => m.Id == id);
            var kaydeden = _context.Users.FirstOrDefaultAsync(u => u.Id == department.CetUserId);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Department.FindAsync(id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.Id == id);
        }
    }
}
