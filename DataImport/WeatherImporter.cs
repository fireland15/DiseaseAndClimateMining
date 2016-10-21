using CsvHelper;
using DataImport.CsvModels;
using DataImport.CsvModels.CsvMaps;
using Remote.DbModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataImport
{
    public class WeatherImporter
    {
        private DataContext _context;
        private Stopwatch _timer;
        private List<Location> _states;

        public WeatherImporter(DataContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            _context = context;
            _states = _context.Locations.ToList();
            _timer = new Stopwatch();
        }

        public void ImportAllWeather()
        {
            Import("ALABAMA", "../../../Data/NOAA/AL.csv");
            Import("ARKANSAS", "../../../Data/NOAA/AR.csv");
            Import("ARIZONA", "../../../Data/NOAA/AZ.csv");
            Import("CALIFORNIA", "../../../Data/NOAA/CA.csv");
            Import("COLORADO", "../../../Data/NOAA/CO.csv");
            Import("CONNECTICUT", "../../../Data/NOAA/CT.csv");
            Import("DELAWARE", "../../../Data/NOAA/DE.csv");
            Import("FLORIDA", "../../../Data/NOAA/FL.csv");
            Import("GEORGIA", "../../../Data/NOAA/GA.csv");
            Import("IOWA", "../../../Data/NOAA/IA.csv");
            Import("IDAHO", "../../../Data/NOAA/ID.csv");
            Import("ILLINOIS", "../../../Data/NOAA/IL.csv");
            Import("INDIANA", "../../../Data/NOAA/IN.csv");
            Import("KANSAS", "../../../Data/NOAA/KS.csv");
            Import("KENTUCKY", "../../../Data/NOAA/KY.csv");
            Import("LOUISIANA", "../../../Data/NOAA/LA.csv");
            Import("MASSACHUSETTS", "../../../Data/NOAA/MA.csv");
            Import("MARYLAND", "../../../Data/NOAA/MD.csv");
            Import("MAINE", "../../../Data/NOAA/ME.csv");
            Import("MICHIGAN", "../../../Data/NOAA/MI.csv");
            Import("MINNESOTA", "../../../Data/NOAA/MN.csv");
            Import("MISSOURI", "../../../Data/NOAA/MO.csv");
            Import("MISSISSIPPI", "../../../Data/NOAA/MS.csv");
            Import("MONTANA", "../../../Data/NOAA/MT.csv");
            Import("NORTH CAROLINA", "../../../Data/NOAA/NC.csv");
            Import("NORTH DAKOTA", "../../../Data/NOAA/ND.csv");
            Import("NEBRASKA", "../../../Data/NOAA/NE.csv");
            Import("NEW HAMPSHIRE", "../../../Data/NOAA/NH.csv");
            Import("NEW JERSEY", "../../../Data/NOAA/NJ.csv");
            Import("NEW MEXICO", "../../../Data/NOAA/NM.csv");
            Import("NEVADA", "../../../Data/NOAA/NV.csv");
            Import("NEW YORK", "../../../Data/NOAA/NY.csv");
            Import("OHIO", "../../../Data/NOAA/OH.csv");
            Import("OKLAHOMA", "../../../Data/NOAA/OK.csv");
            Import("OREGON", "../../../Data/NOAA/OR.csv");
            Import("PENNSYLVANIA", "../../../Data/NOAA/PA.csv");
            Import("RHODE ISLAND", "../../../Data/NOAA/RI.csv");
            Import("SOUTH CAROLINA", "../../../Data/NOAA/SC.csv");
            Import("SOUTH DAKOTA", "../../../Data/NOAA/SD.csv");
            Import("TENNESSEE", "../../../Data/NOAA/TN.csv");
            Import("TEXAS", "../../../Data/NOAA/TX.csv");
            Import("UTAH", "../../../Data/NOAA/UT.csv");
            Import("VIRGINIA", "../../../Data/NOAA/VA.csv");
            Import("VERMONT", "../../../Data/NOAA/VT.csv");
            Import("WASHINGTON", "../../../Data/NOAA/WA.csv");
            Import("WISCONSIN", "../../../Data/NOAA/WI.csv");
            Import("WEST VIRGINIA", "../../../Data/NOAA/WV.csv");
            Import("WYOMING", "../../../Data/NOAA/WY.csv");

            Console.WriteLine($"{_context.WeatherRecords.Count()} weather record imported");
        }

        public void Import(string state, string filename)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentNullException(nameof(filename));

            Console.WriteLine($"Importing weather data for {state} state");

            _timer.Start();
            var newRecordsCount = 0;
            using (DataContext db = new DataContext("DataMiningProjectDb"))
            using (TextReader txtRdr = File.OpenText(filename))
            {
                CsvReader csvRdr = new CsvReader(txtRdr);
                csvRdr.Configuration.RegisterClassMap<WeatherStationMap>();
                IEnumerable<CsvWeatherRecord> records = csvRdr.GetRecords<CsvWeatherRecord>();

                var count = _context.WeatherRecords.Count();

                db.WeatherRecords.AddRange(records.Select(r => ConvertToWeatherRecord(state, r)));
                db.SaveChanges();

                newRecordsCount = _context.WeatherRecords.Count() - count;
            }

            _timer.Stop();

            Console.WriteLine($"\t{_timer.ElapsedMilliseconds}ms {newRecordsCount} weather records imported for {state}");
            _timer.Reset();
        }

        private WeatherRecord ConvertToWeatherRecord(string state, CsvWeatherRecord record)
        {

            WeatherRecord weatherRecord = new WeatherRecord();
            weatherRecord.Location = _states.Single(s => s.Name.Equals(state, StringComparison.CurrentCultureIgnoreCase));

            int year = Convert.ToInt32(record.Date.Trim().Substring(0, 4));
            int month = Convert.ToInt32(record.Date.Trim().Substring(4, 2));
            int day = Convert.ToInt32(record.Date.Trim().Substring(6, 2));
            weatherRecord.Date = new DateTime(year, month, day);

            weatherRecord.MeanTemperature = Convert.ToSingle(Regex.Replace(record.MeanTemperature, "[A-Za-z *]", ""));
            weatherRecord.MinimumTemperature = Convert.ToSingle(Regex.Replace(record.MinimumTemperature, "[A-Za-z *]", ""));
            weatherRecord.MaximumTemperature = Convert.ToSingle(Regex.Replace(record.MaximumTemperature, "[A-Za-z *]", ""));
            weatherRecord.Dewpoint = Convert.ToSingle(record.Dewpoint);
            weatherRecord.SeaLevelPressure = Convert.ToSingle(record.SeaLevelPressure.Trim() == "9999.9" ? null : record.SeaLevelPressure);
            weatherRecord.StationPressure = Convert.ToSingle(record.StationPressure.Trim() == "9999.9" ? null : record.StationPressure);
            weatherRecord.Visibility = Convert.ToSingle(record.Visibility);
            weatherRecord.WindSpeed = Convert.ToSingle(Regex.Replace(record.WindSpeed, "[A-Za-z *]", ""));
            weatherRecord.MaxWindSpeed = Convert.ToSingle(Regex.Replace(record.MaxWindSpeed, "[A-Za-z *]", ""));
            weatherRecord.Precipitation = Convert.ToSingle(Regex.Replace(record.Precipitation, "[A-Za-z *]", ""));
            weatherRecord.SnowDepth = Convert.ToSingle(record.SnowDepth.Trim() == "999.9" ? "0" : record.SnowDepth);
            weatherRecord.HasFog = record.WeatherPhonomena[0] == '1';
            weatherRecord.HasRainOrDrizzle = record.WeatherPhonomena[1] == '1';
            weatherRecord.HasSnowOrIce = record.WeatherPhonomena[2] == '1';
            weatherRecord.HasHail = record.WeatherPhonomena[3] == '1';
            weatherRecord.HasThunder = record.WeatherPhonomena[4] == '1';
            weatherRecord.HasTornado = record.WeatherPhonomena[5] == '1';

            return weatherRecord;
        }
    }
}
