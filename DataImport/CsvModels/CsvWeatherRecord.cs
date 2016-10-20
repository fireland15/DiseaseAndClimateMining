namespace DataImport.CsvModels
{
    public class CsvWeatherRecord
    {
        public string StationNumber { get; set; }
        public string Date { get; set; }
        public string MeanTemperature { get; set; }
        public string MinimumTemperature { get; set; }
        public string MaximumTemperature { get; set; }
        public string Dewpoint { get; set; }
        public string SeaLevelPressure { get; set; }
        public string StationPressure { get; set; }
        public string Visibility { get; set; }
        public string WindSpeed { get; set; }
        public string MaxWindSpeed { get; set; }
        public string Precipitation { get; set; }
        public string SnowDepth { get; set; }
        public string WeatherPhonomena { get; set; }
    }
}
