using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUI.Models
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? PhotoUrl { get; set; }
        [NotMapped]
        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
    }
}
