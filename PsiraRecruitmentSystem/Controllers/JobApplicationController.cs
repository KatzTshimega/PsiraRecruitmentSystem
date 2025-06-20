using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsiraRecruitmentSystem.Data;
using PsiraRecruitmentSystem.Models;

namespace PsiraRecruitmentSystem.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /JobApplication/List
        public async Task<IActionResult> List()
        {
            var jobs = await _context.JobPosts.ToListAsync();
            return View(jobs);
        }

        // GET: /JobApplication/Apply/5
        public async Task<IActionResult> Apply(int id)
        {
            var job = await _context.JobPosts.FirstOrDefaultAsync(j => j.Id == id);
            if (job == null) return NotFound();

            return View(job);
        }

        // POST: /JobApplication/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(int jobPostId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get the applicant's profile using the UserId
            var applicant = await _context.ApplicantDetails
                .FirstOrDefaultAsync(a => a.UserId == userId.Value);

            if (applicant == null)
            {
                //return NotFound("Applicant profile not found.");
                return RedirectToAction("Dashboard", "Applicant");
            }

            // Check if the applicant has already applied for this job
            bool alreadyApplied = await _context.JobApplications
                .AnyAsync(a => a.JobPostId == jobPostId && a.ApplicantDetailsId == applicant.Id);

            if (alreadyApplied)
            {
                TempData["Message"] = "You already applied for this job.";
                return RedirectToAction("MyApplications", "JobApplication");
            }

            // Create and save the application
            var application = new JobApplication
            {
                JobPostId = jobPostId,
                ApplicantDetailsId = applicant.Id,
                DateApplied = DateTime.UtcNow
            };

            _context.JobApplications.Add(application);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Application submitted successfully!";
            return RedirectToAction("MyApplications", "JobApplication");
        }


        // GET: /JobApplication/MyApplications
        public async Task<IActionResult> MyApplications()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get the ApplicantDetails ID for the current user
            var applicant = await _context.ApplicantDetails
                .FirstOrDefaultAsync(a => a.UserId == userId.Value);

            if (applicant == null)
            {
                //return NotFound("Applicant profile not found.");
                return RedirectToAction("Dashboard", "Applicant");
            }

            var applications = await _context.JobApplications
                .Include(a => a.JobPost)
                .Where(a => a.ApplicantDetailsId == applicant.Id)
                .ToListAsync();

            return View(applications);
        }

    }
}
