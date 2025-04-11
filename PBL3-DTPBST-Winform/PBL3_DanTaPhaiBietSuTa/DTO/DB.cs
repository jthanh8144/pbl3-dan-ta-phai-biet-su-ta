using PBL3_DanTaPhaiBietSuTa.DTO;
using System;
using System.Data.Entity;
using System.Linq;

namespace PBL3_DanTaPhaiBietSuTa
{
    public class DB : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PBL3_DanTaPhaiBietSuTa.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public DB()
            : base("name=DB")
        {
            Database.SetInitializer<DB>(new CreateDB());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<GameProcess> GameProcesses { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Point> Points { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}