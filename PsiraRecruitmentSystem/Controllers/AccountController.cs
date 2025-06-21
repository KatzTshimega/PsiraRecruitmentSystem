using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PsiraRecruitmentSystem.Data;
using PsiraRecruitmentSystem.Models;
using PsiraRecruitmentSystem.ViewModels.Account;

namespace PsiraRecruitmentSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IWebHostEnvironment _environment;
        public AccountController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _environment = environment;
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("", "Email already registered.");
                return View(model);
            }

            var user = new User
            {
                Email = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Username = model.Username,
                IdNumber = model.IdNumber,
                CellPhoneNumber = model.CellPhoneNumber,
                WorkNumber = model.WorkNumber,
                HomeAddress = model.HomeAddress,
                PostalCode = model.PostalCode,
                Province = model.Province,
                Role = "Applicant",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                CvPath = ""
            };



            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Registration successful. Please log in.";
            return RedirectToAction("Login");
        }

        // GET: /Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: /Accounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserEditViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return NotFound();

                user.Email = model.Email;
                user.Role = model.Role;
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Username = model.Username;
                user.CellPhoneNumber = model.CellPhoneNumber;
                user.WorkNumber = model.WorkNumber;
                user.HomeAddress = model.HomeAddress;
                user.Province = model.Province;
                user.IdNumber = model.IdNumber;
                user.PostalCode = model.PostalCode;

                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Dashboard", "Applicant");
            }

            return View(model);
        }



        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            bool verificationResult = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            if (!verificationResult)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            // Store login info in session
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("Name", user.Name);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Surname", user.Surname);

            // Redirect by role
            if (user.Role == "Admin")
                return RedirectToAction("Dashboard", "Admin");
            else
                return RedirectToAction("Dashboard", "Applicant");
        }

        [HttpGet]
        public async Task<IActionResult> UploadCv()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value);
            if (user == null)
                return NotFound("User not found.");

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadCv(IFormFile cvFile)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value);
            if (user == null)
                return NotFound("User not found.");

            if (cvFile != null && cvFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/cvs");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid() + "_" + Path.GetFileName(cvFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await cvFile.CopyToAsync(fileStream);
                }

                // Save relative path
                user.CvPath = Path.Combine("uploads/cvs", uniqueFileName);
                _context.Update(user);
                await _context.SaveChangesAsync();

                TempData["Message"] = "CV uploaded successfully!";
            }
            else
            {
                TempData["Message"] = "Please select a file.";
            }

            return RedirectToAction("UploadCv");
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
