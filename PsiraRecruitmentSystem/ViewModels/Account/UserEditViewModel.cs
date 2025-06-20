using System.ComponentModel.DataAnnotations;

namespace PsiraRecruitmentSystem.ViewModels.Account
{
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string CellPhoneNumber { get; set; }
        public string WorkNumber { get; set; }
        public string HomeAddress { get; set; }
        public string Province { get; set; }
        public string IdNumber { get; set; }
        public string PostalCode { get; set; }
    }

}
