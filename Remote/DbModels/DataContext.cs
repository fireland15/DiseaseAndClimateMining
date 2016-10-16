using System.Data.Entity;

namespace Remote.DbModels
{
    public class DataContext : DbContext
    {
        public DataContext() : base("database") { }

        virtual public DbSet<DiseaseRecord> DiseaseRecords { get; set; }
    }
}
