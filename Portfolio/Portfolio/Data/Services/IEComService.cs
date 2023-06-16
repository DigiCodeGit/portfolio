using Portfolio.Models;

// Interface only
namespace Portfolio.Data.Services
{
    public interface IEComService
    {
        /*** Get ***/
        // Artwork
        public IEnumerable<Artwork> GetAllArt();
        public Artwork GetArtById(int id);

        // Cart
        public Cart GetCartById(int id);

        // Cart item
        public CartItem GetCartItemById(int id);

        // User cart item
        public IEnumerable<CartItem> GetUserCartItemByArtId(string userId, int artId);

        // User cart
        public List<(int Key, float Price, int Qty)> GetAllUserCartItems(string userId);
        /*** - ***/

        /*** Insert ***/
        public void AddUserCartItem(string userId, int artId, int qty);
        /*** - ***/

        /*** Update ***/
        public void UpdateUserCartItemQty(CartItem userCartItem);
        /*** - ***/

    }
}
