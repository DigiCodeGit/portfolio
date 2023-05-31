using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    public class CartItem
    {
        /*** Keys ***/
        // My/Cart Item key
        [Key]
        public int CartItemKey { get; set; }

        // Child Key
        public int Key { get; set; }  // Artwork key
        [ForeignKey("Key")]
        public Artwork Artwork { get; set; } // Get Artwork key from this table
        /*** - ***/


        /*** Data ***/
        public int Qty { get; set; }
        /*** - ***/
    }
}
