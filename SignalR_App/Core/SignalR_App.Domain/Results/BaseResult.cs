using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_App.Domain.Results
{
    public class BaseResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
