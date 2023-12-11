namespace SignalR_App.Domain.Results
{
    public class DataResult<TData> : BaseResult
    {
        public TData Data { get; set; }
        public static DataResult<TData> Successed(TData data, string message = "")
        {

            return new DataResult<TData>()
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
        public static DataResult<TData> Failed(string message = "")
        {

            return new DataResult<TData>()
            {
                Success = false,
                Message = message,
                ErrorMessages = new() { message }
            };
        }
    }
}
