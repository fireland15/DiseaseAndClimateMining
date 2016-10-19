using System.ComponentModel.DataAnnotations;

namespace Remote.DbModels
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
