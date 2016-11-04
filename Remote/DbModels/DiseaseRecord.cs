using System.ComponentModel.DataAnnotations;

namespace Remote.DbModels
{
    public class DiseaseRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DiseaseName { get; set; }
        
        [Required]
        public string Location { get; set; }
        
        [Required]
        public string Year { get; set; }
        
        [Required]
        public int Week { get; set; }

        [Required]
        public int NewInfections { get; set; }
    }
}
