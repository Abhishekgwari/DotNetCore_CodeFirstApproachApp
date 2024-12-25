using DotNetCore_CodeFirstApproachApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DotNetCore_CodeFirstApproachApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDBContext studentDb;

        public HomeController(StudentDBContext StudentDb)
        {
            studentDb = StudentDb;
        }

        public async Task<IActionResult> Index()

        {
            var stdData = await studentDb.Students.ToListAsync();
            return View(stdData);
        }


        public IActionResult Create()

        {
            List<SelectListItem> Gender = new()
            {
                new SelectListItem{ Value = "Male",Text= "Male"},
                new SelectListItem{ Value = "Female",Text= "Female"}
            };
            ViewBag.Gender = Gender;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)

        {
            if (ModelState.IsValid)
            {
            await    studentDb.Students.AddAsync(std);
              await  studentDb.SaveChangesAsync();
                TempData["Insert_success"] = "Inserted";
                return  RedirectToAction("Index","Home");
            }

            return View(std);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id==null || studentDb.Students == null)
            {
                return NotFound();
            }
            var stdData = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (stdData == null) { 
            return NotFound();
            }

            return View(stdData);
        }

        public async Task<IActionResult> Edit(int? id)

        {
            List<SelectListItem> Gender = new()
            {
                new SelectListItem{ Value = "Male",Text= "Male"},
                new SelectListItem{ Value = "Female",Text= "Female"}
            };
            ViewBag.Gender = Gender;

            if (id == null || studentDb.Students == null)
            {
                return NotFound();
            }
            var stdData = await studentDb.Students.FindAsync(id);

            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]                   /* for security*/
        public async Task<IActionResult> Edit(int? id, Student std)
        {
            if(id!= std.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                studentDb.Update(std);
                await studentDb.SaveChangesAsync();
                TempData["Update_success"] = "Updated";
                return RedirectToAction("Index","Home");
            }
            return View(std);

        }

        public async Task<IActionResult> Delete(int? id )
        {
            if (id == null || studentDb.Students == null)
            {
                return NotFound();
            }
            var stdData = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var stdData = await studentDb.Students.FindAsync(id);

            if (stdData != null)
            {
                studentDb.Students.Remove(stdData);
                
            }
            await studentDb.SaveChangesAsync();
            TempData["Delete_success"] = "Deleted";

            return RedirectToAction("Index", "Home");

            
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
