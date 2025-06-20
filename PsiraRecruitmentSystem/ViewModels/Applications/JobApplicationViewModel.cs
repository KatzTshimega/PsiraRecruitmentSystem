namespace PsiraRecruitmentSystem.ViewModels.Applications
{
    public class JobApplicationViewModel
    {
        public int JobPostId { get; set; }

        public string JobTitle { get; set; }

        public bool AlreadyApplied { get; set; }

        public string Message { get; set; }

        public ApplicantProfileViewModel Profile { get; set; }
    }
}
