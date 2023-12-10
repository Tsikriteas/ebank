using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MellonBank.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FisrtName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Afm { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        public ICollection<AccountData> Accounts { get; set; }
    }
}
