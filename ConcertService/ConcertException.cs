using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertService
{
    public class ConcertException : Exception
    {

        public ConcertException(string msg) : base(msg)
        {
        }

    }
}
