using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Model
{
    public class CreateUrlViewModel
    {
    
        public string? Url { get; set; }
        [Required]
        public int Time { get; set; }
        public IFormFile? ImgFile { get; set; } 

        //public string? Img { get; set; }
        public int? Priority { get; set; }
    }
    public enum ResultUrl 
    {
        Success,
        notfound,
        eror,
        Duplicate,
        Required

    }

}
