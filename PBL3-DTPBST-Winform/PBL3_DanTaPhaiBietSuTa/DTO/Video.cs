using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3_DanTaPhaiBietSuTa.DTO
{
    [Table("Video")]
    public class Video
    {
        [Key]
        public int VideoID { get; set; }
        [Required]
        public string VideoName { get; set; }
        [Required]
        public string VideoLink { get; set; }
    }
}
