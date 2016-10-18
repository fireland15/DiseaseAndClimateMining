using CsvHelper.Configuration;

namespace DataImport.CsvModels.CsvMaps
{
    public class HaemophilusInfluenzaMap : CsvClassMap<CsvDiseaseRecord>
    {
        public override void CreateMap()
        {
            Map(m => m.Location).Name(" Reporting Area");
            Map(m => m.Year).Name("MMWR Year");
            Map(m => m.Week).Name("MMWR Week");
            Map(m => m.NewInfections).Name("Haemophilus influenzae, invasive†, All ages, all serotypes,Current week");
        }
    }
}
