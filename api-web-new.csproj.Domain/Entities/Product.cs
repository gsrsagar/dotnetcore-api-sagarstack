using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_web_new.Models
{
    
    public class Product
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(10),MaxLength(20)]
        public string Name { get; set; }
    }
}
