using Remote.DbModels;
using System;
using System.Collections.Generic;

namespace Remote.CsvModels
{
    public class ChlamydiaToCoccidioidomycosisDiseaseRecord
    {
        public string ReportingArea { get; set; }
        public string MmwrYear { get; set; }
        public string MmwrWeek { get; set; }
        public string ChlamydiaCurrentWeek { get; set; }
        public string ChlamydiaCurrentWeekFlag { get; set; }
        public string ChlamydiaPreviousYearMedian { get; set; }
        public string ChlamydiaPreviousYearFlags { get; set; }
        public string ChlamydiaPreviousYearMax { get; set; }
        public string ChlamydiaPreviousYearMaxFlags { get; set; }
        public string ChlamydiaCumulative { get; set; }
        public string ChlamydiaCumulativeFlags { get; set; }
        public string ChlamydiaPreviousYearCumulative { get; set; }
        public string ChlamydiaPreviousYearCumulativeFlags { get; set; }
        public string CoccidioidomycosisCurrentWeek { get; set; }
        public string CoccidioidomycosisCurrentWeekFlags { get; set; }
        public string CoccidioidomycosisPreviousYearMedian { get; set; }
        public string CoccidioidomycosisPreviousYearMedianFlags { get; set; }
        public string CoccidioidomycosisPreviousYearMax { get; set; }
        public string CoccidioidomycosisPreviousYearMaxFlags { get; set; }
        public string CoccidioidomycosisCumulative { get; set; }
        public string CoccidioidomycosisCumulativeFlags { get; set; }
        public string CoccidioidomycosisPreviousYearCumulative { get; set; }
        public string CoccidioidomycosisPreviousYearCumulativeFlags { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }

        DiseaseRecord GetChlamydia()
        {
            DiseaseRecord record = new DiseaseRecord();

            record.DiseaseName = "chlamydia";
            record.Year = MmwrYear;
            record.Week = Convert.ToInt32(MmwrWeek);
            record.Location = ReportingArea;
            record.NewInfections = Convert.ToInt32(ChlamydiaCurrentWeek);

            return record;
        }

        DiseaseRecord GetCoccidioidomycosis()
        {
            DiseaseRecord record = new DiseaseRecord();

            record.DiseaseName = "coccidioidomycosis";
            record.Year = MmwrYear;
            record.Week = Convert.ToInt32(MmwrWeek);
            record.Location = ReportingArea;
            record.NewInfections = Convert.ToInt32(CoccidioidomycosisCurrentWeek);

            return record;
        }
    }
}
