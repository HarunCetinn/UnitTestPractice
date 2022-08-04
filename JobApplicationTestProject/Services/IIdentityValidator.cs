using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTestProject.Services
{
    public interface IIdentityValidator
    {
        bool IsValid(string identityNumber);

        bool CheckConnectionToRemoteServer();

        ICountryDataProvider CountryDataProvider { get; }

        public interface ICountryData
        {
            string Country { get; }
        }

        public interface ICountryDataProvider
        {
            ICountryData CountryData { get; }
        }

    }
}
