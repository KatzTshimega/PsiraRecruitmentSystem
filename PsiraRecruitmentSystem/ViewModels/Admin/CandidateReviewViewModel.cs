namespace PsiraRecruitmentSystem.ViewModels.Admin
{
    public class CandidateReviewViewModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Province { get; set; }

        public bool HasDriversLicense { get; set; }

        public string HighestQualification { get; set; }

        public int TotalExperienceYears { get; set; }

        public int TotalPoints { get; set; }

        public bool MeetsRequirements { get; set; }

        public string CvFilePath { get; set; }
    }
}
