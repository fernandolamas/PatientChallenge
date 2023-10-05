using PatientChallenge.Shared.Model;

namespace PatientChallenge.Service.AccountService
{
    public interface IAccountService
    {
        public Task<UserToken> RetrieveToken(UserLogin userLogin);
    }
}
