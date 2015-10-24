using Microsoft.Data.Entity;
using Microsoft.Framework.Configuration;

namespace RandomActs.Models
{
    public class RAOKContext : DbContext
    {
        public DbSet<RandomActs.Models.RandomAct> RandomActs { get; set; }
        public DbSet<RandomActs.Models.RandomActor> RandomActors { get; set; }
        public DbSet<RandomActs.Models.RandomActActor> RandomActActors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.;Database=RandomActs5DB;Trusted_Connection=True;");
        }
    }
}
