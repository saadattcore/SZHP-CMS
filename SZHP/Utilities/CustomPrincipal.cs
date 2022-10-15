using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SZHPCMS.Utilities
{

    public interface ICustomPrincipal : IPrincipal
    {

        long UserID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }

    public class CustomPrincipal : ICustomPrincipal
    {
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IIdentity Identity { get; set; }


        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}