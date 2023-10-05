using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using PatientChallenge.Service.EncryptService;
using PatientChallenge.Shared.Model;
using System.Data;
using System.Data.SqlClient;

namespace PatientChallenge.Service.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly string connectionString;
        private readonly IEncryptService _encryptService;

        public PatientService(IConfiguration configuration, IEncryptService encryptService)
        {
            connectionString = configuration["ConnectionStrings:PatientDB"];
            _encryptService = encryptService;
        }

        public async Task<long> CreatePatient(Patient patient)
        {
            try
            {
                patient.Password = _encryptService.HashPassword(patient.Password);

                if(await UserNameExists(patient.UserName))
                {
                    throw new DuplicateNameException(patient.UserName);
                }
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    int id = await conn.InsertAsync(new Patient
                    {
                        Name = patient.Name,
                        UserName = patient.UserName,
                        Password = patient.Password,
                        IsActive = patient.IsActive,
                        Role = patient.Role
                    });
                    if (id == 0)
                    {
                        throw new ArgumentException("Invalid data provided");
                    }
                    return id;
                }

            }
            catch (Exception) { throw; }
        }

        public async Task<Patient?> GetPatient(long id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                return await conn.GetAsync<Patient>(id);
            }
        }

        public async Task<Patient?> UpdatePatient(Patient newPatient)
        {
            try
            {
                if (await UserNameExists(newPatient.UserName))
                {
                    throw new DuplicateNameException(newPatient.UserName);
                }
                Patient? existingPatient = await GetPatient(newPatient.Id);
                if (existingPatient == null)
                {
                    return null;
                }
                existingPatient.Password = _encryptService.HashPassword(newPatient.Password);
                existingPatient.Name = newPatient.Name;
                existingPatient.UserName = newPatient.UserName;
                existingPatient.Password = newPatient.Password;
                existingPatient.IsActive = newPatient.IsActive;
                existingPatient.Role = newPatient.Role;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    var res = await conn.UpdateAsync(existingPatient);
                    if (res == false)
                    {
                        throw new ArgumentException("Invalid data provided");
                    }
                    return newPatient;
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> DeletePatient(long id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                return await conn.DeleteAsync(new Patient() { Id = id });
            }
        }
        public async Task<bool> UserNameExists(string username)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = $"SELECT count(UserName) from Patients p where p.UserName = '{username}';";
                if(await conn.ExecuteScalarAsync<int>(query) == 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
