using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PatientChallenge.Controllers;
using PatientChallenge.Service.PatientService;
using PatientChallenge.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientChallenge.UnitTest
{
    public class PatientTest
    {
        public PatientContext CreatePatientContext()
        {
            var patientService = new Mock<IPatientService>();
            var controller = new PatientController(patientService.Object);
            return new PatientContext(){ PatientService = patientService, PatientController = controller };
        }

        [Fact]
        public async Task CreatePatient_ShouldReturnCreatedAtActionResult()
        {
            //Since
            PatientContext patientContext = CreatePatientContext();

            Patient patient = new Patient()
            {
                Name = "Paulo",
                UserName = "paulo94",
                Password = "apwd1234",
                IsActive = true,
                Role = Role.User
            };
            patientContext.PatientService.Setup(s => s.CreatePatient(patient)).ReturnsAsync(patient.Id);

            //When
            var result = await patientContext.PatientController.CreatePatient(patient);

            //Then
            Assert.IsType<CreatedAtActionResult>(result);
            var okResult = result as CreatedAtActionResult;
            Assert.NotNull(okResult.Value);
        }
        [Fact]
        public async Task GetPatient_ShouldReturnOkObjectResult()
        {
            //Since
            PatientContext patientContext = CreatePatientContext();

            long existingPatient = 1;
            patientContext.PatientService.Setup(s => s.GetPatient(existingPatient)).ReturnsAsync(new Patient());

            //When
            var result = await patientContext.PatientController.GetPatient(existingPatient);

            //Then
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task UpdatePatient_ShouldReturnOkObjectResult()
        {
            //Since
            PatientContext patientContext = CreatePatientContext();
            Patient patientToModify = new Patient()
            {
                Id = 1,
                Name = "Administrator",
                UserName = "admin5553",
                Password = "4dmin",
                IsActive = true,
                Role = Role.Administrator
            };
            patientContext.PatientService.Setup(s => s.UpdatePatient(patientToModify)).ReturnsAsync(patientToModify);

            //When
            var result = await patientContext.PatientController.UpdatePatient(patientToModify);


            //Then
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(patientToModify,okResult.Value);

        }
        [Fact]
        public async Task DeletePatient_ShouldReturnOkResult()
        {
            //Since
            PatientContext patientContext = CreatePatientContext();
            long existingPatient = 1;
            patientContext.PatientService.Setup(s => s.DeletePatient(existingPatient)).ReturnsAsync(true);

            //When 
            var result = await patientContext.PatientController.DeletePatient(existingPatient);

            //Then
            Assert.IsType<OkResult>(result);
            var okResult = result as OkResult;
            Assert.Equal(200,okResult.StatusCode);
        }
    }
}
