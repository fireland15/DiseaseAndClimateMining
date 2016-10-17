using DataImport.CsvModels.CsvMaps;
using Remote.DbModels;

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
            using (DataContext context = new DataContext())
            {
                DiseaseImporter importer = new DiseaseImporter(context);

                importer.Import<ChlamydiaMap>("chlamydia", Files[0]);
                importer.Import<ChlamydiaMap>("chlamydia", Files[1]);
            }
        }
    }
}
