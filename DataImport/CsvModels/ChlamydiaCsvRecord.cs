using Remote.DbModels;
using System;

namespace DataImport.CsvModels
{
    public class ChlamydiaCsvRecord : IDiseaseCsvRecord
    {
        public string ReportingArea { get; set; }
        public string Year { get; set; }
        public string Week { get; set; }
        public string NewInfections { get; set; }

        public override string ToString()
        {
            return $"Chlamydia {Year}-{Week} {ReportingArea} {NewInfections}";
        }

        public DiseaseRecord ToDiseaseRecord()
        {
            return new DiseaseRecord
            {
                DiseaseName = "chlamydia",
                Location = ReportingArea,
                Year = Year,
                Week = Convert.ToInt32(Week != string.Empty ? Week : "0"),
                NewInfections = Convert.ToInt32(NewInfections != string.Empty ? NewInfections : "0")
            };
        }
    }
}
