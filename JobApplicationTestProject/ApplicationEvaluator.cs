using JobApplicationLibrary.Models;
using JobApplicationTestProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary
{
    public class ApplicationEvaluator
    {
        private const int minAge = 18;
        private List<string> techStackList = new() { "C#", "RabbitMq", "Microservices", "Go", };
        private const int autoAcceptedYearofExperiance = 15;
        private readonly IIdentityValidator _identityValidator;
        //private IdentityValidator identityValidator;

        public ApplicationEvaluator(IIdentityValidator identityValidator)
        {
            
            _identityValidator = identityValidator;
        }

        public ApplicationResult Evaluate(JobApplication form)
        {
            if (form.Applicant.Age < minAge)
                return ApplicationResult.AutoRejected;

            if(_identityValidator.CountryDataProvider.CountryData.Country != "Turkey")
                return ApplicationResult.TransferredToCTO;

            //var connectionSucceed = _identityValidator.CheckConnectionToRemoteServer();
            var validIdentity = /*connectionSucceed &&*/ _identityValidator.IsValid(form.Applicant.IdentityNumber);

            if(!validIdentity)
                return ApplicationResult.TransferredToHR;

            var sr = GetTechStackSimilarytyRate(form.TechStackList);
            if (sr < 25)
                return ApplicationResult.AutoRejected;
            if (sr > 75 && form.YearsOfExperiance > autoAcceptedYearofExperiance)
                return ApplicationResult.AutoAccepted;


            return ApplicationResult.AutoAccepted;
        }

        private int GetTechStackSimilarytyRate(List<string> techStacks)
        {
            var matchedCount = techStackList.Where(i => techStackList.Contains(i, StringComparer.OrdinalIgnoreCase)).Count();

            return (int)((double)matchedCount / techStackList.Count) * 100;
        }


    }

    public enum ApplicationResult
    {
        AutoRejected,
        TransferredToHR,
        TransferredToLead,
        TransferredToCTO,
        AutoAccepted
    }


}
