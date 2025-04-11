using PBL3_DanTaPhaiBietSuTa.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3_DanTaPhaiBietSuTa
{
    public class CreateDB : CreateDatabaseIfNotExists<DB>
    {
        protected override void Seed(PBL3_DanTaPhaiBietSuTa.DB context)
        {
            
        }
        
    }
}
