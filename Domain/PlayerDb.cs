using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class PlayerDb : IdentityUser
    {
        public string Name { get; set; }
        public bool IsReady { get; set; }
        public bool IsGoing { get; set; }
        public int MoveCount { get; set; }
        public virtual GameDb? Game { get; set; }
    }
}