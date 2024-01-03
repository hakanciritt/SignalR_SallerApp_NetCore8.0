using Microsoft.AspNetCore.Identity;

namespace SignalR_App.Domain.Entitites
{
    public class AppUser : IdentityUser<long>
    {
        public string Name { get; set; }
        public string SurName { get; set; }

    }
}
