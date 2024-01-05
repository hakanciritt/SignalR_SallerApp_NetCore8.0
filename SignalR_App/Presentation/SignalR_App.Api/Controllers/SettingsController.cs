using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalR_App.Application.Dtos.SettingDtos;
using SignalR_App.Application.Services.Abstracts;

namespace SignalR_App.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SettingsController(ISettingService settingService) : ApiControllerBase
    {
        private readonly ISettingService _settingService = settingService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _settingService.GetAll();
            return Ok(result);
        }

        [HttpGet("detail")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _settingService.GetById(id);
            return ActionResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(SettingDto setting)
        {
            var result = await _settingService.Update(setting);
            return ActionResult(result);
        }
    }
}
