using JobApplicationLibrary.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace JobApplicationLibrary.UnitTest
{
    public class ApplicationEvaluateUnitTest
    {


        [Test]
        public void Application_ShouldTransferredToAutoRejected_WithUnderAge()
        {
            //Arreange

            var evaluator = new ApplicationEvaluator();
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

        //[Test]
        //public void Application_TransferredToAutoRejected_WithNoTechStack()
        //{
        //    //Arreange

        //    var evaluator = new ApplicationEvaluator();
        //    var form = new JobApplication()
        //    {
        //        Applicant = new Applicant() { Age = 19 },
        //        TechStackList = new List<string>() { "" }
        //        //YearsOfExperiance = 10

        //    };

        //    //Action

        //    var appResult = evaluator.Evaluate(form);

        //    //Assert

        //    Assert.AreEqual(appResult, ApplicationResult.AutoRejected);

        //}


        [Test]
        public void Application_ShouldTransferredToAutoAccepted_WithTechStackOver75p()
        {
            //Arreange

            var evaluator = new ApplicationEvaluator();
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 19 },
                TechStackList = new List<string>() { "C#", "RabbitMq", "Microservices", "Go", },
                YearsOfExperiance = 16

            };

            //Action

            var appResult = evaluator.Evaluate(form);

            //Assert

            Assert.AreEqual(appResult, ApplicationResult.AutoAccepted);

        }




    }
}