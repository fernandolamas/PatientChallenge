using Dapper;
using Microsoft.Extensions.Configuration;
using PatientChallenge.Service.EncryptService;
using PatientChallenge.Shared.Helper;
using PatientChallenge.Shared.Model;
using System.Data.SqlClient;

namespace PatientChallenge.Service.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly string connectionString;
        private readonly IEncryptService _encryptService;

        public AccountService(JwtSettings jwtSettings, IConfiguration configuration, IEncryptService encryptService) { 
            _jwtSettings = jwtSettings;
            connectionString = configuration["ConnectionStrings:PatientDB"];
            _encryptService = encryptService;
        }
        public async Task<UserToken> RetrieveToken(UserLogin userLogin)
        {
            try{
                var Token = new UserToken();
                Patient? patient;
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * from Patients p where p.UserName = '{userLogin.UserName}'";
                    patient = await conn.QueryFirstOrDefaultAsync<Patient>(query);
                }
                if(patient == null)
                {
                    throw new ArgumentException("Invalid credentials");
                }
                bool isPasswordCorrect = _encryptService.VerifyPassword(userLogin.Password, patient.Password ?? string.Empty);
                if (isPasswordCorrect)
                {
                    Token = JwtHelper.GenTokenKey(
                        new UserToken()
                        {
                            UserName = patient.Name,
                            Id = patient.Id,
                            GuidId = Guid.NewGuid(),
                            Role = patient.Role
                        }, _jwtSettings);

                    return Token;
                }
                else
                {
                    throw new ArgumentException("Invalid credentials");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
