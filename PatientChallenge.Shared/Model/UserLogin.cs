using System.ComponentModel.DataAnnotations;

namespace PatientChallenge.Shared.Model
{
    public class UserLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
