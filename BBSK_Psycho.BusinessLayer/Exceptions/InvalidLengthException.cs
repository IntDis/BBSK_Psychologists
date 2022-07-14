using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Exceptions;

public class InvalidLengthException : Exception
{
    public InvalidLengthException(string message) : base(message) { }
}
