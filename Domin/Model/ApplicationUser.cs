using Microsoft.AspNetCore.Identity;

namespace Domin.Model
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UrlModel> Urls { get; set; }

    }
}
