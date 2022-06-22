using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_okul.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [Range(1, 12)]
        [Display(Name = "Class Number")]
        public string Class { get; set; }
        [Required]
        [StringLength(1)]
        [Display(Name = "Class Letter")]
        public string ClassLetter { get; set; }
        [Required]
        public string Section { get; set; }
        public int? FirstGrade { get; set; }
        public int? SecondGrade { get; set; }
        public string? LetterGrade { get; set; }
    }
}