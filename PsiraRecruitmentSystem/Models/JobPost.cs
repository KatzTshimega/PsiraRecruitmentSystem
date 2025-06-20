using System.ComponentModel.DataAnnotations;

namespace PsiraRecruitmentSystem.Models
{
    public class JobPost
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BusinessUnit { get; set; }
        public string ManagerNames { get; set; }
        public string ManagerEmailAddress { get; set; }
        public int RequiredExperienceYears { get; set; }
        public string RequiredQualification { get; set; }
        public string RequiredDriversLicense { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public DateTime DatePosted { get; set; }
    }
}