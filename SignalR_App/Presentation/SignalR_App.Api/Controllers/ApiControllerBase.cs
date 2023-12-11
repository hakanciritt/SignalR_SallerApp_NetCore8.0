using Microsoft.AspNetCore.Mvc;
using SignalR_App.Domain.Result;
using SignalR_App.Domain.Results;

namespace SignalR_App.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected ObjectResult ActionResult(Result result)
        {
            return result.Success ? Ok(result) : BadRequest(result);
        }
        protected ObjectResult ActionResult<TData>(DataResult<TData> result)
        {
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
