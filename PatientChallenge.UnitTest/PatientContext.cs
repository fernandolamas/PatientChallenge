using Moq;
using PatientChallenge.Controllers;
using PatientChallenge.Service.PatientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientChallenge.UnitTest
{
    public class PatientContext
    {
        public Mock<IPatientService> PatientService { get; set; } = null!;
        public PatientController PatientController { get; set; } = null!;
    }
}
