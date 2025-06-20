using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsiraRecruitmentSystem.Data;
using PsiraRecruitmentSystem.Models;

namespace PsiraRecruitmentSystem.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class JobPostController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /JobPost
        public async Task<IActionResult> JobPosts()
        {
            var jobPosts = await _context.JobPosts.ToListAsync();
            return View(jobPosts);
        }

        // GET: JobPost/ClosedPosts
        public async Task<IActionResult> ClosedPosts()
        {
            var closedJobPosts = await _context.JobPosts
                .Where(j => j.ClosingDate < DateTime.UtcNow)
                .ToListAsync();

            return View(closedJobPosts);
        }


        // GET: /JobPost/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /JobPost/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobPost model)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (ModelState.IsValid)
            {
                model.DatePosted = DateTime.Now;
                _context.JobPosts.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(JobPosts));
            }
            return View(model);
        }

        // GET: /JobPost/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var job = await _context.JobPosts.FindAsync(id);
            if (job == null)
                return NotFound();

            return View(job);
        }

        // POST: /JobPost/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobPost model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(JobPosts));
            }
            return View(model);
        }

        // GET: /JobPost/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var job = await _context.JobPosts.FirstOrDefaultAsync(j => j.Id == id);
            if (job == null)
                return NotFound();

            return View(job);
        }

        // GET: JobPost/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _context.JobPosts.FindAsync(id);
            if (job == null)
                return NotFound();

            return View(job);
        }

        // POST: JobPost/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.JobPosts.FindAsync(id);
            if (job != null)
            {
                _context.JobPosts.Remove(job);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(JobPosts));
        }
    }
}
