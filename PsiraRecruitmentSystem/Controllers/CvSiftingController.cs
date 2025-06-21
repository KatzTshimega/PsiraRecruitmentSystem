using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsiraRecruitmentSystem.Data;
using PsiraRecruitmentSystem.Models;

namespace PsiraRecruitmentSystem.Controllers
{
    public class CvSiftingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CvSiftingController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  /Admin/SiftingOfCVs
        public async Task<IActionResult> CvSifting()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Load all job applications along with related user and applicant details
            var applications = await _context.JobApplications
                .Include(j => j.Applicant)
                .ThenInclude(a => a.User)
                .Include(j => j.JobPost)
                .Where(j => j.Applicant.User.Role != "Admin")
                .ToListAsync();

            var viewModel = new List<CvSiftingViewModel>();

            foreach (var application in applications)
            {
                var applicantDetails = application.Applicant;
                var user = applicantDetails?.User;
                var jobPost = application.JobPost;

                if (user == null || applicantDetails == null || jobPost == null)
                    continue;

                // === Scoring logic ===
                int totalPoints = 0;

                // 1. Driver’s License
                if (applicantDetails.DriversLicense?.ToLower() == "yes")
                    totalPoints += 2;

                // 2. Qualification
                if (string.Equals(applicantDetails.HighestQualification?.ToLower(), jobPost.RequiredQualification?.ToLower())
                    || QualificationIsHigher(applicantDetails.HighestQualification, jobPost.RequiredQualification))
                {
                    totalPoints += 2;
                }

                // 3. Experience
                if (applicantDetails.TotalExperienceInYears >= jobPost.RequiredExperienceYears)
                    totalPoints += 2;

                // ViewModel Entry 
                viewModel.Add(new CvSiftingViewModel
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Province = user.Province,
                    DriversLicense = applicantDetails.DriversLicense,
                    HighestQualification = applicantDetails.HighestQualification,
                    TotalExperience = applicantDetails.TotalExperienceInYears,
                    TotalPoints = totalPoints,
                    MeetRequirements = totalPoints == 6 ? "Yes" : "No",
                    CvPath = user.CvPath
                });
            }

            return View(viewModel);
        }

        // Optional: Helper to compare qualifications (simplified logic)
        private bool QualificationIsHigher(string applicantQualification, string requiredQualification)
        {
            var rank = new List<string> {
            "grade 12", "certificate", "diploma", "degree", "honours", "masters", "phd"
        };

            var applicantRank = rank.IndexOf(applicantQualification?.ToLower() ?? "");
            var requiredRank = rank.IndexOf(requiredQualification?.ToLower() ?? "");

            return applicantRank > requiredRank;
        }
    }
}
