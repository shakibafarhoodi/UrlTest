using System.ComponentModel.DataAnnotations;

namespace web.ViewModel
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "username")]
        [StringLength(200)]
        public string UserName{ get; set; }
        //[Required]
        //[EmailAddress]

        //public string  Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Rember Me?")]
        public bool RemeberMe { get; set; }
    }
}
