using PatientChallenge.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
