using CsvHelper.Configuration;

namespace DataImport.CsvModels.CsvMaps
{
    public class CoccidioidomycosisMap : CsvClassMap<CoccidioidomycosisCsvRecord>
    {
        public override void CreateMap()
        {
            Map(m => m.ReportingArea).Name("Reporting Area");
            Map(m => m.Year).Name("MMWR Year");
            Map(m => m.Week).Name("MMWR Week");
            Map(m => m.NewInfections).Name("Coccidioidomycosis, Current week");
        }
    }
}
