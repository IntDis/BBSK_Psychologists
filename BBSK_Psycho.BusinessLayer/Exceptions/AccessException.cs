using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Exceptions
{
    public class AccessException : Exception
    {
        public AccessException(string message) : base(message) { }
    }
}
