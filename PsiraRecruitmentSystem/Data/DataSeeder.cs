using PsiraRecruitmentSystem.Models;

namespace PsiraRecruitmentSystem.Data
{
    public static class DataSeeder
    {
        public static void SeedAdminUser(ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.Email == "hr@psira.co.za"))
            {
                var admin = new User
                {
                    Email = "hr@psira.co.za",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("HRPassword"),
                    Role = "Admin",
                    Name = "Natilie",
                    Surname = "Jameson",
                    IdNumber = "785845525088",
                    CellPhoneNumber = "081454787",
                    WorkNumber = "0123456789",
                    HomeAddress = "Admin Office",
                    Province = "Gauteng",
                    PostalCode = "0001",
                    CvPath = ""
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
