using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsiraRecruitmentSystem.Models
{
    public class ApplicantDetails
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }  

        public string HighestQualification { get; set; }
        public string DriversLicense { get; set; }
        public string CurrentPosition { get; set; }
        public string? CurrentCompany { get; set; }
        public string? YearsInCurrentPosition { get; set; }
        public string CurrentSalary { get; set; }
        public int TotalExperienceInYears { get; set; }
        public string PreviousPosition { get; set; }
        public string PreviousCompany { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }

    }
}