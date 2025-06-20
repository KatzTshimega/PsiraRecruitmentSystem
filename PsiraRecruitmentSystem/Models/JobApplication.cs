using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsiraRecruitmentSystem.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int JobPostId { get; set; }

        [ForeignKey("JobPostId")]
        public JobPost JobPost { get; set; }

        [Required]
        public int ApplicantDetailsId { get; set; }
     
        [ForeignKey("ApplicantDetailsId")]
        public ApplicantDetails? Applicant { get; set; }

        public DateTime DateApplied { get; set; } = DateTime.UtcNow;

        public int TotalPoints { get; set; }
        public bool MeetsRequirements { get; set; }
    }
}