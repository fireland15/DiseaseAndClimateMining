﻿using CsvHelper.Configuration;

namespace DataImport.CsvModels.CsvMaps
{
    public class WestNileVirusDiseaseMap2014 : CsvClassMap<CsvDiseaseRecord>
    {
        public override void CreateMap()
        {
            Map(m => m.Location).Name("Reporting Area");
            Map(m => m.Year).Name("MMWR Year");
            Map(m => m.Week).Name("MMWR Week");
            Map(m => m.NewInfections).Name("West Nile virus disease†, Neuroinvasive, Current week");
        }
    }

    public class WestNileVirusDiseaseMap2015 : CsvClassMap<CsvDiseaseRecord>
    {
        public override void CreateMap()
        {
            Map(m => m.Location).Name("Reporting Area");
            Map(m => m.Year).Name("MMWR Year");
            Map(m => m.Week).Name("MMWR Week");
            Map(m => m.NewInfections).Name("West Nile virus disease§, Neuroinvasive, Current week");
        }
    }
}