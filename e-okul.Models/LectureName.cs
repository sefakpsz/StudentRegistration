using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_okul.Models
{
    public class LectureName
    {
        [Key]
        public int Id { get; set; }
        public int StudenId { get; set; }
        public int LectureId { get; set; }
    }
}
