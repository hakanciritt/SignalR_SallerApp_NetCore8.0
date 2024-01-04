using SignalR_App.Domain.Results;

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
        public static Result Failed(List<string> errorMessages)
        {

            return new Result()
            {
                Success = false,
                Message = errorMessages?.First(),
                ErrorMessages = errorMessages
            };
        }
    }
}
