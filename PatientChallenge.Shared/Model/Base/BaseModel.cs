using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace PatientChallenge.Shared.Model.Base
{
    public class BaseModel
    {
        [SwaggerSchema(ReadOnly = true)]
        [Key]
        public long Id { get; set; }
    }
}
