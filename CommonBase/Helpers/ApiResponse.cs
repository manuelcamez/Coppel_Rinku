using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase.Helpers
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public object Data { get; set; }
    }
}
