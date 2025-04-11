using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3_DanTaPhaiBietSuTa.DTO
{
    [Table("Question")]
    public class Question
    {
        [Key]
        [Required]
        public int QuestionID { get; set; }
        [Required]
        public int StageID { get; set; }
        [Required]
        public string QuestionContent { get; set; }
        [Required]
        public string KeyA { get; set; }
        [Required]
        public string KeyB { get; set; }
        [Required]
        public string KeyC { get; set; }
        [Required]
        public string KeyD { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public int TimeStop { get; set; }
        [ForeignKey("StageID")]
        public virtual Stage Stage { get; set; }
    }
}
