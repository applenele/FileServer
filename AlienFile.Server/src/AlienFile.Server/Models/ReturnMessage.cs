using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlienFile.Server.Models
{
    public class ReturnMessage
    {
        public bool IsSuccess { set; get; }

        public string Message { set; get; }

        public string Path { set; get; }

        public string RealPath { set; get; }
    }
}
