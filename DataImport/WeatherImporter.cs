using CsvHelper;
using DataImport.CsvModels;
using DataImport.CsvModels.CsvMaps;
using Remote.DbModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        public void ImportAllWeather(string dataDirectory)
        {
            Import("ALABAMA",       dataDirectory + "/NOAA/AL.csv");
            Import("ARKANSAS",      dataDirectory + "/NOAA/AR.csv");
            Import("ARIZONA",       dataDirectory + "/NOAA/AZ.csv");
            Import("CALIFORNIA",    dataDirectory + "/NOAA/CA.csv");
            Import("COLORADO",      dataDirectory + "/NOAA/CO.csv");
            Import("CONNECTICUT",   dataDirectory + "/NOAA/CT.csv");
            Import("DELAWARE",      dataDirectory + "/NOAA/DE.csv");
            Import("FLORIDA",       dataDirectory + "/NOAA/FL.csv");
            Import("GEORGIA",       dataDirectory + "/NOAA/GA.csv");
            Import("IOWA",          dataDirectory + "/NOAA/IA.csv");
            Import("IDAHO",         dataDirectory + "/NOAA/ID.csv");
            Import("ILLINOIS",      dataDirectory + "/NOAA/IL.csv");
            Import("INDIANA",       dataDirectory + "/NOAA/IN.csv");
            Import("KANSAS",        dataDirectory + "/NOAA/KS.csv");
            Import("KENTUCKY",      dataDirectory + "/NOAA/KY.csv");
            Import("LOUISIANA",     dataDirectory + "/NOAA/LA.csv");
            Import("MASSACHUSETTS", dataDirectory + "/NOAA/MA.csv");
            Import("MARYLAND",      dataDirectory + "/NOAA/MD.csv");
            Import("MAINE",         dataDirectory + "/NOAA/ME.csv");
            Import("MICHIGAN",      dataDirectory + "/NOAA/MI.csv");
            Import("MINNESOTA",     dataDirectory + "/NOAA/MN.csv");
            Import("MISSOURI",      dataDirectory + "/NOAA/MO.csv");
            Import("MISSISSIPPI",   dataDirectory + "/NOAA/MS.csv");
            Import("MONTANA",       dataDirectory + "/NOAA/MT.csv");
            Import("NORTH CAROLINA", dataDirectory + "/NOAA/NC.csv");
            Import("NORTH DAKOTA",  dataDirectory + "/NOAA/ND.csv");
            Import("NEBRASKA",      dataDirectory + "/NOAA/NE.csv");
            Import("NEW HAMPSHIRE", dataDirectory + "/NOAA/NH.csv");
            Import("NEW JERSEY",    dataDirectory + "/NOAA/NJ.csv");
            Import("NEW MEXICO",    dataDirectory + "/NOAA/NM.csv");
            Import("NEVADA",        dataDirectory + "/NOAA/NV.csv");
            Import("NEW YORK",      dataDirectory + "/NOAA/NY.csv");
            Import("OHIO",          dataDirectory + "/NOAA/OH.csv");
            Import("OKLAHOMA",      dataDirectory + "/NOAA/OK.csv");
            Import("OREGON",        dataDirectory + "/NOAA/OR.csv");
            Import("PENNSYLVANIA",  dataDirectory + "/NOAA/PA.csv");
            Import("RHODE ISLAND",  dataDirectory + "/NOAA/RI.csv");
            Import("SOUTH CAROLINA", dataDirectory + "/NOAA/SC.csv");
            Import("SOUTH DAKOTA",  dataDirectory + "/NOAA/SD.csv");
            Import("TENNESSEE",     dataDirectory + "/NOAA/TN.csv");
            Import("TEXAS",         dataDirectory + "/NOAA/TX.csv");
            Import("UTAH",          dataDirectory + "/NOAA/UT.csv");
            Import("VIRGINIA",      dataDirectory + "/NOAA/VA.csv");
            Import("VERMONT",       dataDirectory + "/NOAA/VT.csv");
            Import("WASHINGTON",    dataDirectory + "/NOAA/WA.csv");
            Import("WISCONSIN",     dataDirectory + "/NOAA/WI.csv");
            Import("WEST VIRGINIA", dataDirectory + "/NOAA/WV.csv");
            Import("WYOMING",       dataDirectory + "/NOAA/WY.csv");

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

                IEnumerable<WeatherRecord> dailyRecords = records.Select(r => ConvertToWeatherRecord(state, r)).ToList();

                IEnumerable<WeatherRecord> weeklyRecords = dailyRecords.CalculateWeeklyAverages();

                db.WeatherRecords.AddRange(weeklyRecords);
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

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            weatherRecord.Week = dfi.Calendar.GetWeekOfYear(weatherRecord.Date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

            string temp = Regex.Replace(record.MeanTemperature, "[A-Za-z *]", "");
            weatherRecord.MeanTemperature = Convert.ToSingle(temp == "9999.9" ? "-1" : temp);

            string minTemp = Regex.Replace(record.MinimumTemperature, "[A-Za-z *]", "");
            weatherRecord.MinimumTemperature = Convert.ToSingle(minTemp == "9999.9" ? "-1" : minTemp);

            string maxTemp = Regex.Replace(record.MaximumTemperature, "[A-Za-z *]", "");
            weatherRecord.MaximumTemperature = Convert.ToSingle(maxTemp == "9999.9" ? "-1" : maxTemp);

            weatherRecord.Dewpoint = Convert.ToSingle(record.Dewpoint == "9999.9" ? "-1" : record.SeaLevelPressure);

            string seaLevelPressure = record.SeaLevelPressure.Trim();
            weatherRecord.SeaLevelPressure = Convert.ToSingle(seaLevelPressure == "9999.9" ? "-1" : seaLevelPressure);

            string stationPressure = record.StationPressure.Trim();
            weatherRecord.StationPressure = Convert.ToSingle(stationPressure == "999.9" ? "-1" : stationPressure);

            string visibility = record.Visibility == "999.9" ? "-1" : record.Visibility;
            weatherRecord.Visibility = Convert.ToSingle(visibility);

            string windSpeed = Regex.Replace(record.WindSpeed, "[A-Za-z *]", "");
            weatherRecord.WindSpeed = Convert.ToSingle(windSpeed == "999.9" ? "-1" : windSpeed);

            string maxWindSpeed = Regex.Replace(record.MaxWindSpeed, "[A-Za-z *]", "");
            weatherRecord.MaxWindSpeed = Convert.ToSingle(maxWindSpeed == "999.9" ? "-1" : maxWindSpeed);

            string precipitation =Regex.Replace(record.Precipitation, "[A-Za-z *]", "");
            weatherRecord.Precipitation = Convert.ToSingle(precipitation == "99.99" ? "-1" : precipitation);

            string snowDepth = Regex.Replace(record.SnowDepth, "[A-Za-z *]", "");
            weatherRecord.SnowDepth = Convert.ToSingle(snowDepth == "999.9" ? "0" : snowDepth);

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
