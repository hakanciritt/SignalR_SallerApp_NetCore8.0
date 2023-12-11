using SignalR_App.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR_App.Domain.Result
{
    public class Result : BaseResult
    {
        public static Result Successed(string message = "")
        {

            return new Result()
            {
                Success = true,
                Message = message,
            };
        }
        public static Result Failed(string message = "")
        {

            return new Result()
            {
                Success = false,
                Message = message,
                ErrorMessages = new() { message }
            };
        }
    }
}
