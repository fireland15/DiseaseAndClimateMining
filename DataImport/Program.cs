using DataImport.CsvModels.CsvMaps;

namespace DataImport
{
    class Program
    {
        private static string[] Files =
        {
            "../../../Data/2016_ChlamydiaToCoccidioidomycosis.csv",
            "../../../Data/2015_ChlamydiaToCoccidioidomycosis.csv"
        };
       
        static void Main(string[] args)
        { 
            DiseaseImporter importer = new DiseaseImporter();

            importer.Import2<ChlamydiaMap>("chlamydia", Files[0]);

        }
    }
}
