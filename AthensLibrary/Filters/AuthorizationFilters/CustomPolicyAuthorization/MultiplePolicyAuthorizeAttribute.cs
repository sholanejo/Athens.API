using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AthensLibrary.Filters.AuthorizationFilters
{
    public class MultiplePolicysAuthorizeAttribute : TypeFilterAttribute
    {
        public MultiplePolicysAuthorizeAttribute(string policys, bool isAnd = false) : base(typeof(MultiplePolicysAuthorizeFilter))
        {
            Arguments = new object[] { policys, isAnd };
        }
    }
}
