using Portfolio.Models;

// Interface only
namespace Portfolio.Data.Services
{
    public interface IEComService
    {
        /*** Get ***/
        // Artwork
        IEnumerable<Artwork> GetAllArt();
        Artwork GetArtById(int id);

        // Cart
        Cart GetCartById(int id);

        // Cart item
        CartItem GetCartItemById(int id);

        // User cart item
        IEnumerable <CartItem> GetUserCartItemByArtId(string userId, int artId);
        /*** - ***/

        /*** Insert ***/
        public void AddUserCartItem(string userId, int artId, int qty);
        /*** - ***/

        /*** Update ***/
        public void UpdateUserCartItemQty(CartItem userCartItem);
        /*** - ***/

    }
}
