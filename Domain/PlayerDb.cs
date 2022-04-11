using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class PlayerDb : IdentityUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GameDb? Game { get; set; }
    }
}