using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Security;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;

namespace AzureSQLWCFService
{
    public class CredentialValidator : UserNamePasswordValidator
    {

        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new SecurityTokenException("Username and password required");
           
            if(userName=="UserName"&&password=="Password")
                throw new FaultException(string.Format("Wrong username ({0}) or password ", userName));
        }
       

    }
}