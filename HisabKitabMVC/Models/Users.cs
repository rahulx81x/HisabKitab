using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HisabKitabMVC.Models
{
    public class Users
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [StringLength(maximumLength:30)]
        [Required(ErrorMessage = "User Name is mandatory.")]
        [DisplayName("User Name")]
        public string UserName { get; set; } = null!;


        [StringLength(maximumLength: 15)]
        [Required(ErrorMessage = "Password is mandatory.")]
        [DisplayName("User password")]
        public string UserPassword { get; set; } = null!;

    }
}
