namespace PatientChallenge.Service.EncryptService
{
    public interface IEncryptService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}
