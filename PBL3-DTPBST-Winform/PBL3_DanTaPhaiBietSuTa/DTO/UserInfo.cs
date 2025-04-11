using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3_DanTaPhaiBietSuTa.DTO
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        public int UserID { get; set; }
        [Required][MaxLength(50)]
        public string Username { get; set; }
        [Required][MaxLength(50)]
        public string Password { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        public override string ToString()
        {
            return Username;
        }
    }
}
