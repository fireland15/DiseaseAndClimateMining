using Remote.DbModels;
using System;
using System.Diagnostics;
using System.IO;

namespace DataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            ClearDatabase();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            StartImportProcess(args[0]);

            watch.Stop();

            double timeToImport = watch.Elapsed.TotalSeconds;

            Console.WriteLine($"Importing finished in {timeToImport} seconds");
        }

        private static void ClearDatabase()
        {
            if (File.Exists("DiseaseAndClimateDb.sdf"))
                File.Delete("DiseaseAndClimateDb.sdf");

            using (DataContext context = new DataContext("DataMiningProjectDb"))
            {
                context.Database.CommandTimeout = 3000;
                context.Database.ExecuteSqlCommand("DELETE FROM [dbo].[DiseaseRecords]");
                context.Database.ExecuteSqlCommand("DELETE FROM [dbo].[Locations]");
                context.Database.ExecuteSqlCommand("DELETE FROM [dbo].[WeatherRecords]");
                context.SaveChanges();
            }
        }

        private static void StartImportProcess(string dataDirectory)
        {
            using (DataContext context = new DataContext("DataMiningProjectDb"))
            {

                Console.WriteLine("Importing Locations");
                LocationImporter locationImporter = new LocationImporter(context);
                locationImporter.ImportStates();

                Console.WriteLine("Importing Diseases");
                DiseaseImporter diseaseImporter = new DiseaseImporter(context);
                diseaseImporter.ImportAllDiseases(dataDirectory);

                Console.WriteLine("Importing Weather");
                WeatherImporter weatherImporter = new WeatherImporter(context);
                weatherImporter.ImportAllWeather(dataDirectory);
            }
        }
    }
}
