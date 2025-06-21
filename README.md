PSiRA e-Recruitment System
==========================

**Author:** Katlego Monamodi ([katztshimega@gmail.com](mailto:katztshimega@gmail.com))

**Project Type:** ASP.NET Core MVC Web Application

**Target Framework:** .NET 8.0

* * *

📋 Project Overview
-------------------

This web application is a recruitment system for the Private Security Industry Regulatory Authority (PSiRA). It allows administrators to manage job posts, sift CVs, and track applications, while applicants can register, upload CVs, and apply for job postings.

* * *

🧰 Technologies & Libraries Used
--------------------------------

 *   **ASP.NET Core MVC** (.NET 8)
 *   **Entity Framework Core** with SQL Server
 *   **Bootstrap 5** for responsive UI
 *   **Font Awesome** and **Bootstrap Icons** for UI icons
 *   **BCrypt.Net** for password hashing
 *   **Session-based authentication** (with role-based navigation)

* * *

⚙️ Setup Instructions
---------------------

### 1\. Clone the Repository

    git clone https://github.com/KatzTshimega/PsiraRecruitmentSystem.git

### 2\. Install Dependencies

Ensure that you have the following installed:

 *   .NET 8 SDK
 *   SQL Server or LocalDB

### 3\. Update Connection String

Edit `appsettings.json` and update your database connection string:

    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER;Database=PsiraRecruitmentDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }

### 4\. Run Migrations & Seed Database

    dotnet ef database update

To seed the database with initial roles and admin user, run (if implemented):

    dotnet run seed

### 5\. Build and Run the Project

    dotnet run

The application will be available at [https://localhost:5001](https://localhost:5001)

* * *

👤 Roles & Access
-----------------

 *   **Admin**: Can manage job posts, view applicants, sift CVs, and manage applications.
 *   **Applicant**: Can register, upload CV, view job posts, and apply.

* * *

📂 Folder Structure
-------------------

 *   **Controllers**: MVC Controllers
 *   **Models**: Database entities
 *   **Views**: Razor Views
 *   **ViewModels**: Form and logic binding models
 *   **wwwroot/uploads/cvs**: Uploaded CVs

* * *

📄 License
----------

This project is proprietary and intended for PSiRA recruitment purposes.