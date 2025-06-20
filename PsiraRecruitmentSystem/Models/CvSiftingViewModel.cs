namespace PsiraRecruitmentSystem.Models
{
    public class CvSiftingViewModel
    {
        public int JobApplicationId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Province { get; set; }

        public string DriversLicense { get; set; }
        public string HighestQualification { get; set; }
        public int TotalExperience { get; set; }

        public int TotalPoints { get; set; }
        public string MeetRequirements { get; set; }

        public string CvPath { get; set; } 
    }
}
