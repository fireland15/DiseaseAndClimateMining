using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Remote.DbModels
{
    public class DiseaseRecord
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string DiseaseName { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        public string Location { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        public string Year { get; set; }

        [Key]
        [Column(Order = 3)]
        [Required]
        public int Week { get; set; }

        [Required]
        public int NewInfections { get; set; }
    }
}
