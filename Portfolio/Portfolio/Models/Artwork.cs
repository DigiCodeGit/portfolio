using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Artwork
    {
        [Key]
        public int Key { get; set; }

        public string ? Url { get; set; } 

        public string ? Title { get; set; }

        [DataType(DataType.Currency)]
        public float Price { get; set; }
    }
}
