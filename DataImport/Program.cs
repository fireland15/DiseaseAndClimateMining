using DataImport.CsvModels.CsvMaps;
using Remote.DbModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            ClearDatabase();
            Stopwatch watch = new Stopwatch();
            watch.Start();

            StartImportProcess();

            watch.Stop();

            float timeToImport = watch.Elapsed.Seconds;

            Console.WriteLine($"Importing finished in {timeToImport} seconds");
        }

        private static void ClearDatabase()
        {
            if (File.Exists("DiseaseAndClimateDb.sdf"))
                File.Delete("DiseaseAndClimateDb.sdf");

            using (DataContext context = new DataContext("DataMiningProjectDb"))
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[DiseaseRecords]");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[Locations]");
                context.SaveChanges();
            }
        }

        private static void StartImportProcess()
        {
            using (DataContext context = new DataContext("DataMiningProjectDb"))
            {
                Console.WriteLine("Importing Locations");
                LocationImporter locationImporter = new LocationImporter(context);
                locationImporter.ImportStates();

                Console.WriteLine("Importing Diseases");
                DiseaseImporter diseaseImporter = new DiseaseImporter(context);
                diseaseImporter.ImportAllDiseases();
            }
        }
    }
}
