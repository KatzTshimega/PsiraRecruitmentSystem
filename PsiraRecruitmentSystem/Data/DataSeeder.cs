using PsiraRecruitmentSystem.Models;

namespace PsiraRecruitmentSystem.Data
{
    public static class DataSeeder
    {
        public static void SeedAdminUser(ApplicationDbContext context)
        {
            // Admin user
            if (!context.Users.Any(u => u.Email == "hr@psira.co.za"))
            {
                var admin = new User
                {
                    Email = "hr@psira.co.za",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("HRPassword"),
                    Role = "Admin",
                    Username = "admin",
                    Name = "Natilie",
                    Surname = "Free",
                    IdNumber = "785845525088",
                    CellPhoneNumber = "081454787",
                    WorkNumber = "0123456789",
                    HomeAddress = "Admin Office",
                    Province = "Gauteng",
                    PostalCode = "0001",
                    CvPath = ""
                };

                context.Users.Add(admin);
            }

            // Applicant 1
            if (!context.Users.Any(u => u.Email == "applicant1@example.com"))
            {
                var applicant1 = new User
                {
                    Email = "applicant1@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123"),
                    Role = "Applicant",
                    Username = "applicant1",
                    Name = "John",
                    Surname = "Doe",
                    IdNumber = "9012123456789",
                    CellPhoneNumber = "0810000001",
                    WorkNumber = "",
                    HomeAddress = "123 Freedom Street",
                    Province = "North West",
                    PostalCode = "0299",
                    CvPath = ""
                };

                context.Users.Add(applicant1);
            }

            // Applicant 2
            if (!context.Users.Any(u => u.Email == "applicant2@example.com"))
            {
                var applicant2 = new User
                {
                    Email = "applicant2@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123"),
                    Role = "Applicant",
                    Username = "applicant2",
                    Name = "Jane",
                    Surname = "Smith",
                    IdNumber = "8808087654321",
                    CellPhoneNumber = "0810000002",
                    WorkNumber = "",
                    HomeAddress = "456 Growth Avenue",
                    Province = "KwaZulu-Natal",
                    PostalCode = "4001",
                    CvPath = ""
                };

                context.Users.Add(applicant2);
            }

            context.SaveChanges();
        }
    }
}
