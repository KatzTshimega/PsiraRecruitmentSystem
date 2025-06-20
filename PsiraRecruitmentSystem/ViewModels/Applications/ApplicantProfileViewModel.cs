using System.ComponentModel.DataAnnotations;

namespace PsiraRecruitmentSystem.ViewModels.Applications
{
    public class ApplicantProfileViewModel
    {
        public string FullName { get; set; }

        [Required]
        public string HighestQualification { get; set; }

        public bool HasDriversLicense { get; set; }

        public string CurrentPosition { get; set; }

        public string CurrentCompany { get; set; }

        public int YearsInCurrentPosition { get; set; }

        public decimal CurrentSalary { get; set; }

        public int TotalExperienceYears { get; set; }

        public string PreviousPosition { get; set; }

        public string PreviousCompany { get; set; }

        public DateTime PeriodFrom { get; set; }

        public DateTime PeriodTo { get; set; }

        public IFormFile CV { get; set; }
    }
}
