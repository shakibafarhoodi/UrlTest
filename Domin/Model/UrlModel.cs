using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Model
{
    public class UrlModel
    {
        [Key]
        public int Id { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }    
        public string ?Url { get; set; }
        public int time { get; set; }
        public string? Img { get; set; } 
        public int? Priority { get; set; }
        //public virtual ICollection<ApplicationUser> Users { get; set; }
        /* =new List<UserModel>();*/

    }
}
