using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramlama.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password",ErrorMessage = "Şifreler uyuşmuyor")]
        public string Confirm { get; set; }
        [NotMapped]
        public byte isAdmin { get; set; }

    }
}
