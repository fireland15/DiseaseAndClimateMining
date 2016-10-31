using CsvHelper.Configuration;
using System;

namespace DataImport.CsvModels.CsvMaps
{
    public class WeatherStationMap : CsvClassMap<CsvWeatherRecord>
    {
        [Obsolete]
        public override void CreateMap()
        {
            Map(m => m.StationNumber).Name("STN---");
            Map(m => m.Date).Name(" YEARMODA");
            Map(m => m.MeanTemperature).Name("   TEMP");
            Map(m => m.MinimumTemperature).Name("  MIN  ");
            Map(m => m.MaximumTemperature).Name("   MAX  ");
            Map(m => m.Dewpoint).Name("   DEWP");
            Map(m => m.SeaLevelPressure).Name("  SLP  ");
            Map(m => m.StationPressure).Name("  STP  ");
            Map(m => m.Visibility).Name(" VISIB");
            Map(m => m.WindSpeed).Name("  WDSP");
            Map(m => m.MaxWindSpeed).Name(" MXSPD");
            Map(m => m.Precipitation).Name("PRCP  ");
            Map(m => m.SnowDepth).Name("SNDP ");
            Map(m => m.WeatherPhonomena).Name(" FRSHTT");
        }
    }
}
