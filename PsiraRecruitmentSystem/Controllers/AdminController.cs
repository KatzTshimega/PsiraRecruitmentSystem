using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsiraRecruitmentSystem.Data;
using PsiraRecruitmentSystem.Models;

namespace PsiraRecruitmentSystem.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Dashboard 
        public IActionResult Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // GET: View all job posts  /Admin/ListJobPosts 
        public async Task<IActionResult> ListJobPosts()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var jobs = await _context.JobPosts.OrderByDescending(j => j.DatePosted).ToListAsync();
            return View(jobs);
        }

        // GET: View all applicants /Admin/Applicants
        public async Task<IActionResult> Applicants()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var applicants = await _context.Users
                .Where(u => u.Role == "Applicant")
                .Include(a => a.ApplicantDetails)
                .ToListAsync();

            return View(applicants);
        }

        // GET: View all applications submitted
        public async Task<IActionResult> Applications()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var applications = await _context.JobApplications
                .Include(a => a.JobPost)
                .Include(a => a.Applicant)
                .ThenInclude(ad => ad.User)
                .ToListAsync();

            return View(applications);
        }

        // Redirect to CV Sifting screen
        public IActionResult CvSifting()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            return RedirectToAction("Index", "CvSifting");
        }
    }
}
