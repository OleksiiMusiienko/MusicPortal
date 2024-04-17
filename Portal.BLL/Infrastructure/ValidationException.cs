using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.Infrastructure
{
    public class ValidationExcept: Exception
    {
        public string Property { get; protected set; }
        public ValidationExcept(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
