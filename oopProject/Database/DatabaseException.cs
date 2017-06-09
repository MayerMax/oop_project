using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooPproject
{
    public class DatabaseException : InvalidOperationException
    {
        public DatabaseException()
        {
        }

        public DatabaseException(string message) : base(message)
        {
        }
    }
}

