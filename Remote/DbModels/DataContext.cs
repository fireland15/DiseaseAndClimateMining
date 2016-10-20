using System.Data.Entity;

namespace Remote.DbModels
{
    public class DataContext : DbContext
    {
        public DataContext() : base("database") { }
        public DataContext(string connectionString) : base(connectionString) { }

        virtual public DbSet<DiseaseRecord> DiseaseRecords { get; set; }
        virtual public DbSet<Location> Locations { get; set; }
        virtual public DbSet<WeatherRecord> WeatherRecords { get; set; }
    }
}
