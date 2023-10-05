using PatientChallenge.Shared.Model.Base;

namespace PatientChallenge.Shared.Model
{
    public enum Role
    {
        Administrator = 0,
        User = 1
    }
    public class UserToken : BaseModel
    {
        public string? Token { get; set; }
        public string UserName { get; set; } = string.Empty;
        public TimeSpan Validity { get; set; }
        public string? RefreshToken { get; set; }
        public Guid GuidId { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public Role Role { get; set; }
    }
}
