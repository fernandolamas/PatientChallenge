using PatientChallenge.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientChallenge.Service.AccountService
{
    public interface IAccountService
    {
        public Task<UserToken> RetrieveToken(UserLogin userLogin);
    }
}
