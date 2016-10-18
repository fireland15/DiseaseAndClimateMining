using DataImport.CsvModels.CsvMaps;
using Remote.DbModels;
using System;
using System.IO;
using System.Linq;

namespace DataImport
{
    class Program
    {
        private static string[] Files =
        {
            "../../../Data/2016_ChlamydiaToCoccidioidomycosis.csv",
            "../../../Data/2015_ChlamydiaToCoccidioidomycosis.csv",
            "../../../Data/2014_CryptosporidiosisToDengue.csv"
        };
       
        static void Main(string[] args)
        {
            ClearDatabase();


            using (DataContext context = new DataContext())
            {
                DiseaseImporter importer = new DiseaseImporter(context);

                importer.Import<ChlamydiaMap>("chlamydia", Files[0]);
                importer.Import<ChlamydiaMap>("chlamydia", Files[1]);
                importer.Import<CryptosporidiosisMap>("cryptosporidiosis", Files[2]);

                Console.WriteLine(context.DiseaseRecords.Count());
                Console.WriteLine(context.DiseaseRecords.Select(x => x.NewInfections).ToArray().Aggregate((a, b) => a + b));
            }
        }

        private static void ClearDatabase()
        {
            if (File.Exists("DiseaseAndClimateDb.sdf"))
                File.Delete("DiseaseAndClimateDb.sdf");
        }
    }
}
