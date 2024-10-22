using System.Drawing.Printing;

namespace web.ViewModel
{
    public class ManageUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } 
    }
}
