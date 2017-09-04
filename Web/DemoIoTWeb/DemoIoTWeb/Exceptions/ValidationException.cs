using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoIoTWeb.Exceptions
{
    public class ValidationException : Exception
    {
        public String Field { get; set; }
        public ValidationException(String field, String message) : base(message)
        {
            this.Field = field;
        }
    }
}