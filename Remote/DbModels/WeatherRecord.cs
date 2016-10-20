﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remote.DbModels
{
    public class WeatherRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LocationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public float MeanTemperature { get; set; }

        [Required]
        public float MinimumTemperature { get; set; }

        [Required]
        public float MaximumTemperature { get; set; }

        [Required]
        public float Dewpoint { get; set; }

        public float SeaLevelPressure { get; set; }

        public float StationPressure { get; set; }
        
        [Required]
        public float Visibility { get; set; }

        [Required]
        public float WindSpeed { get; set; }

        [Required]
        public float MaxWindSpeed { get; set; }

        [Required]
        public float Precipitation { get; set; }

        [Required]
        public float SnowDepth { get; set; }

        [Required]
        public bool HasFog { get; set; }

        [Required]
        public bool HasRainOrDrizzle { get; set; }

        [Required]
        public bool HasSnowOrIce { get; set; }

        [Required]
        public bool HasHail { get; set; }

        [Required]
        public bool HasThunder { get; set; }

        [Required]
        public bool HasTornado { get; set; }

        [ForeignKey(nameof(LocationId))]
        public virtual Location Location { get; set; }
    }
}