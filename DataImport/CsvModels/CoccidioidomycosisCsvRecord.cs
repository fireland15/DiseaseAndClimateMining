using Remote.DbModels;
using System;

namespace DataImport.CsvModels
{
    public class CoccidioidomycosisCsvRecord : IDiseaseCsvRecord
    {
        public string ReportingArea { get; set; }
        public string Year { get; set; }
        public string Week { get; set; }
        public string NewInfections { get; set; }

        public override string ToString()
        {
            return $"Coccidioidomycosis {Year}-{Week} {ReportingArea} {NewInfections}";
        }

        public DiseaseRecord ToDiseaseRecord()
        {
            return new DiseaseRecord
            {
                DiseaseName = "coccidioidomycosis",
                Location = ReportingArea,
                Year = Year,
                Week = Convert.ToInt32(Week != string.Empty ? Week : "0"),
                NewInfections = Convert.ToInt32(NewInfections != string.Empty ? NewInfections : "0")
            };
        }
    }
}
