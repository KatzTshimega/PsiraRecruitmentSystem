using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsiraRecruitmentSystem.Data;
using PsiraRecruitmentSystem.Models;

namespace PsiraRecruitmentSystem.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Applicant/Applicants
        public async Task<IActionResult> Dashboard()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var applicants = await _context.ApplicantDetails
                .Include(a => a.User) // Eager-load User
                .Where(a => a.UserId == userId) // Optionally filter by current user
                .ToListAsync();

            return View(applicants);
        }


        // GET: /JobPost/ListJobPosts
        public async Task<IActionResult> ListJobPosts()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var jobPosts = await _context.JobPosts.ToListAsync();
            return View(jobPosts);
        }


        // GET: /Applicant/Create
        public IActionResult Create()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: /Applicant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicantDetails applicant)
        {
            var userId = applicant.UserId;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            ViewBag.CurrentUser = user;
            applicant.User = user;

            // Check if applicant details already exist for this user
            var existingApplicant = await _context.ApplicantDetails
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (existingApplicant != null)
            {
                // Redirect to edit instead of creating a duplicate
                return RedirectToAction("Edit", new { id = existingApplicant.Id });
            }

           // ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }

            return View(applicant);
        }

        //[Authorize(Roles = "Applicant")]
        // GET: /Applicant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
                return NotFound();

            var applicant = await _context.ApplicantDetails.FindAsync(id);
            if (applicant == null)
                return NotFound();

            return View(applicant);
        }

        // POST: /Applicant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicantDetails applicant)
        {
            var userIds = HttpContext.Session.GetInt32("UserId");

            if (userIds == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = applicant.UserId;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            applicant.User = user;

            if (id != applicant.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ApplicantDetails.Any(e => e.Id == applicant.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Dashboard));
            }
            return View(applicant);
        }


        // GET: /Applicant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
                return NotFound();

            var applicant = await _context.ApplicantDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
                return NotFound();

            return View(applicant);
        }
    }
}
