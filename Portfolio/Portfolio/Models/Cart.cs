using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    public class Cart
    {
        // My/Cart key
        [Key]
        public int CartKey { get; set; }

        // Child Key
        public int CartItemKey { get; set; }  // Cart Item key

        [ForeignKey("CartItemKey")]
        public CartItem CartItem { get; set; } // Get Cart Item key from this table

        // User
        public string UserKey { get; set; } // Session key, since no user logins
    }
}
