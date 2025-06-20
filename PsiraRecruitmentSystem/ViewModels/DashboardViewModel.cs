using PsiraRecruitmentSystem.ViewModels.Jobs;

namespace PsiraRecruitmentSystem.ViewModels
{
    public class DashboardViewModel
    {
        public string UserRole { get; set; }

        public int JobPostsCount { get; set; }

        public int ApplicationsCount { get; set; }

        public List<JobPostViewModel> RecentJobs { get; set; }
    }

}
