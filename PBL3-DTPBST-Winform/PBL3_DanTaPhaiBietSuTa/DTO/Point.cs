using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PBL3_DanTaPhaiBietSuTa.DTO
{
    [Table("Point")]
    public class Point
    {
        [Key]
        public int PointID { get; set; }
        [Required]
        public int StageID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int point { get; set; }
    }
}
