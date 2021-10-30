using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthensLibrary.Model.Helpers.HelperClasses
{
    public sealed class ReturnModel
    {
        public ReturnModel()
        {

        }
        public ReturnModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }       
    }
}
