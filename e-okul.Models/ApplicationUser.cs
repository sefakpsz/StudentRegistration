using System.ComponentModel.DataAnnotations;

namespace e_okul.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Role { get; set; }
    }
}