using JobApplicationLibrary.Models;
using NUnit.Framework;
using System.Collections.Generic;
using Moq;
using JobApplicationTestProject.Services;
using static JobApplicationTestProject.Services.IIdentityValidator;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {


        [Test]
        public void Application_ShouldTransferredToAutoRejected_WithUnderAge()
        {
            //Arreange

            var evaluator = new ApplicationEvaluator(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 17
                }
            };

            //Action

            var appResult = evaluator.Evaluate(form);

            //Assert

            Assert.AreEqual(appResult, ApplicationResult.AutoRejected);

        }

        [Test]
        public void Application_TransferredToAutoRejected_WithNoTechStack()
        {
            //Arreange

            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Loose);
            mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 17 , IdentityNumber="Any string..."},
                TechStackList = new List<string>() { "" }
                //YearsOfExperiance = 10

            };

            //Action

            var appResult = evaluator.Evaluate(form);

            //Assert

            Assert.AreEqual(appResult, ApplicationResult.AutoRejected);

        }


        [Test]
        public void Application_ShouldTransferredToAutoAccepted_WithTechStackOver75p()
        {
            //Arreange

            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Loose);
            mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(i => i.CountryDataProvider.CountryData.Country).Returns("Turkey");
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 19 },
                TechStackList = new List<string>() { "C#", "RabbitMq", "Microservices", "Go", },
                YearsOfExperiance = 16

            };

            //Action

            var appResult = evaluator.Evaluate(form);

            //Assert

            Assert.AreEqual(ApplicationResult.AutoAccepted ,appResult);

        }

        [Test]
        public void Application_TransferredToHR_WithInValidIdentityNumber()
        {
            //Arreange

            //var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Loose); Gevþek yaklaþým
            //mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false); Strict ise sýký yaklaþým, kuralcý.

            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Strict);

            mockValidator.DefaultValue = DefaultValue.Mock;
            mockValidator.Setup(i => i.CountryDataProvider.CountryData.Country).Returns("Turkey");
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);
            //mockValidator.Setup(i => i.CheckConnectionToRemoteServer()).Returns(false);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 19 }
            };

            //Action

            var appResult = evaluator.Evaluate(form);

            //Assert

            Assert.AreEqual(ApplicationResult.TransferredToHR, appResult);

        }

        [Test]
        public void Application_TransferredToCTO_WithOfficeLocation()
        {
            //Arreange

            //var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Loose); Gevþek yaklaþým
            //mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false); Strict ise sýký yaklaþým, kuralcý.

            var mockValidator = new Mock<IIdentityValidator>();
            mockValidator.Setup(i => i.CountryDataProvider.CountryData.Country).Returns("Grecee");

            //var mockCountryData = new Mock<ICountryData>();
            //mockCountryData.Setup(i => i.Country).Returns("Grecee");

            //var mockProvider = new Mock<ICountryDataProvider>();
            //mockProvider.Setup(i=>i.CountryData).Returns(mockCountryData.Object);

            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            

            
            //mockValidator.Setup(Ý=>Ý.Country).Returns("Grecee");
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 19 },
                
            };

            //Action

            var appResult = evaluator.Evaluate(form);

            //Assert

            Assert.AreEqual(ApplicationResult.TransferredToCTO, appResult);

        }



    }
}