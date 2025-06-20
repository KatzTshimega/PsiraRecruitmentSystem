using System.ComponentModel.DataAnnotations;

namespace PsiraRecruitmentSystem.ViewModels.Jobs
{
    public class JobPostViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string QualificationRequirement { get; set; }

        public int RequiredExperienceYears { get; set; }

        public DateTime ClosingDate { get; set; }

        public string BusinessUnit { get; set; }
    }
}
