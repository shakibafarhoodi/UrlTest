using System.ComponentModel.DataAnnotations;

namespace web.ViewModel
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
