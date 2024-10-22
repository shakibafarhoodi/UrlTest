using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Domin.Model
{
    public class RegisterViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Remote("IsAnyEmail", "Account", HttpMethod = "Post", AdditionalFields = "__RequestVerificationToken")]
        public string Email { get; set; }
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string Repassword { get; set; }

    }
}
