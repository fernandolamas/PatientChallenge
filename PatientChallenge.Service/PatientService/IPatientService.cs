using PatientChallenge.Shared.Model;

namespace PatientChallenge.Service.PatientService
{
    public interface IPatientService
    {
        public Task<long> CreatePatient(Patient patient);
        public Task<Patient?> GetPatient(long id);
        public Task<Patient?> UpdatePatient(Patient patient);
        public Task<bool> DeletePatient(long id);
    }
}
