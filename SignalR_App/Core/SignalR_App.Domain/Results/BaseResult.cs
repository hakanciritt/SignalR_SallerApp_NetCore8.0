namespace SignalR_App.Domain.Results
{
    public class BaseResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
