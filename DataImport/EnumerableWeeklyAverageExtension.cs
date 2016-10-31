using Remote.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataImport
{
    public static class EnumerableWeeklyAverageExtension
    {
        public static IEnumerable<WeatherRecord> CalculateWeeklyAverages(this IEnumerable<WeatherRecord> recordsByDay)
        {
            ICollection<WeatherRecord> result = new List<WeatherRecord>();

            foreach (var year in recordsByDay.Select(r => r.Date.Year).Distinct())
            {
                IEnumerable<WeatherRecord> recordsForYear = recordsByDay.Where(r => r.Date.Year == year);

                foreach (var week in recordsForYear.Select(r => r.Week).Distinct())
                {
                    IEnumerable<WeatherRecord> recordsForWeek = recordsForYear.Where(r => r.Week == week);

                    ICollection<WeatherRecord> weekOfRecords = new List<WeatherRecord>();

                    var x = recordsForWeek.Select(r => r.Date.DayOfWeek).Distinct();

                    foreach (var day in recordsForWeek.Select(r => r.Date.DayOfWeek).Distinct())
                    {
                        IEnumerable<WeatherRecord> dailyRecords = recordsForWeek.Where(r => r.Date.DayOfWeek == day);

                        WeatherRecord averageDayRecord = new WeatherRecord();

                        try
                        {
                            averageDayRecord.Dewpoint = dailyRecords.Select(r => r.Dewpoint).Where(d => d != -1.00F).Average();
                            averageDayRecord.Location = dailyRecords.First().Location;
                            averageDayRecord.MaximumTemperature = dailyRecords.Select(r => r.MaximumTemperature).Where(d => d != -1.00F).Average();
                            averageDayRecord.MaxWindSpeed = dailyRecords.Select(r => r.MaxWindSpeed).Where(d => d != -1.00F).Average();
                            averageDayRecord.MeanTemperature = dailyRecords.Select(r => r.MeanTemperature).Where(d => d != -1.00F).Average();
                            averageDayRecord.MinimumTemperature = dailyRecords.Select(r => r.MinimumTemperature).Where(d => d != -1.00F).Average();
                            averageDayRecord.Precipitation = dailyRecords.Select(r => r.Precipitation).Where(d => d != -1.00F).Average();
                            averageDayRecord.SeaLevelPressure = dailyRecords.Select(r => r.SeaLevelPressure).Where(d => d != -1.00F).Average();
                            averageDayRecord.SnowDepth = dailyRecords.Select(r => r.SnowDepth).Where(d => d != -1.00F).Average();
                            averageDayRecord.StationPressure = dailyRecords.Select(r => r.StationPressure).Where(d => d != -1.00F).Average();
                            averageDayRecord.Visibility = dailyRecords.Select(r => r.Visibility).Where(d => d != -1.00F).Average();
                            averageDayRecord.WindSpeed = dailyRecords.Select(r => r.WindSpeed).Where(d => d != -1.00F).Average();

                            averageDayRecord.HasFog = dailyRecords.Select(r => r.HasFog).Any(b => b);
                            averageDayRecord.HasRainOrDrizzle = dailyRecords.Select(r => r.HasRainOrDrizzle).Any(b => b);
                            averageDayRecord.HasSnowOrIce = dailyRecords.Select(r => r.HasSnowOrIce).Any(b => b);
                            averageDayRecord.HasHail = dailyRecords.Select(r => r.HasHail).Any(b => b);
                            averageDayRecord.HasThunder = dailyRecords.Select(r => r.HasThunder).Any(b => b);
                            averageDayRecord.HasTornado = dailyRecords.Select(r => r.HasTornado).Any(b => b);

                            weekOfRecords.Add(averageDayRecord);
                        }
                        catch { }
                    }

                    if (weekOfRecords.Count() > 0)
                    {
                        result.Add(new WeatherRecord
                        {
                            Year = year,
                            Week = week,
                            Location = weekOfRecords.First().Location,
                            Dewpoint = weekOfRecords.Select(r => r.Dewpoint).Average(),
                            MaximumTemperature = weekOfRecords.Select(r => r.MaximumTemperature).Average(),
                            MaxWindSpeed = weekOfRecords.Select(r => r.MaxWindSpeed).Average(),
                            MeanTemperature = weekOfRecords.Select(r => r.MeanTemperature).Average(),
                            MinimumTemperature = weekOfRecords.Select(r => r.MinimumTemperature).Average(),
                            Precipitation = weekOfRecords.Select(r => r.Precipitation).Average(),
                            SeaLevelPressure = weekOfRecords.Select(r => r.SeaLevelPressure).Average(),
                            StationPressure = weekOfRecords.Select(r => r.StationPressure).Average(),
                            Visibility = weekOfRecords.Select(r => r.Visibility).Average(),
                            WindSpeed = weekOfRecords.Select(r => r.WindSpeed).Average(),
                            HasFog = weekOfRecords.Select(r => r.HasFog).Any(b => b),
                            HasRainOrDrizzle = weekOfRecords.Select(r => r.HasRainOrDrizzle).Any(b => b),
                            HasSnowOrIce = weekOfRecords.Select(r => r.HasSnowOrIce).Any(b => b),
                            HasHail = weekOfRecords.Select(r => r.HasHail).Any(b => b),
                            HasThunder = weekOfRecords.Select(r => r.HasThunder).Any(b => b),
                            HasTornado = weekOfRecords.Select(r => r.HasTornado).Any(b => b)
                        });
                    }
                }
            }

            return result;
        }
    }
}
