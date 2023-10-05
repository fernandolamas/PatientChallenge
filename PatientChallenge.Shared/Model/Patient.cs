using PatientChallenge.Shared.Model.Base;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace PatientChallenge.Shared.Model
{
    public class Patient : BaseModel
    {
        [Required, StringLength(100, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        public string Name { get; set; } = null!;
        [Required, StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        public string UserName { get; set; } = null!;
        [Required, StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        [SwaggerSchema(ReadOnly = true)]
        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }
    }
}
