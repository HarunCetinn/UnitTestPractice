using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationTestProject.Services
{
    public class IdentityValidator :IIdentityValidator
    {
        //Bu sınıf bizim için önemsiz şu an.

        public string Country => throw new NotImplementedException();

        public IIdentityValidator.ICountryDataProvider CountryDataProvider => throw new NotImplementedException();

        public bool CheckConnectionToRemoteServer()
        {
            throw new NotImplementedException();
        }

        public bool IsValid(string identityNumber)
        {
            return true;
        }


    }
}
