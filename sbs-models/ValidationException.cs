using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbs_api.models
{
    public class ValidationException : Exception
    {
        public string ValidationMessage = "";
        public ValidationException(string msg):base(msg)
        {
            ValidationMessage = msg;
        }
    }
}
