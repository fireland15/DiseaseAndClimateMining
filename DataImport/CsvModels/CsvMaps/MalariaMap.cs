﻿using CsvHelper.Configuration;
using System;

namespace DataImport.CsvModels.CsvMaps
{
    public class MalariaMap : CsvClassMap<CsvDiseaseRecord>
    {
        [Obsolete]
        public override void CreateMap()
        {
            Map(m => m.Location).Name("Reporting Area");
            Map(m => m.Year).Name("MMWR Year");
            Map(m => m.Week).Name("MMWR Week");
            Map(m => m.NewInfections).Name("Malaria, Current week");
        }
    }
}