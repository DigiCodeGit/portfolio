using Microsoft.EntityFrameworkCore;
using Portfolio.Migrations;
using Portfolio.Models;

// Service that implements the interface
namespace Portfolio.Data.Services
{
    public class ECommerceService : IEComService
    {
        private readonly PortalECommerceDbSql _dbSql;

        // Inject sql translator into constructor so controller can use it
        public ECommerceService(PortalECommerceDbSql dbSql)
        {
            _dbSql = dbSql;
        }


        /*** Get ***/
        public IEnumerable<Artwork> GetAllArt()
        {
            // Retrieve data from database
            var dataArtwork = _dbSql.Artwork.ToList();

            return (dataArtwork);
        }

        public Artwork GetArtById(int id)
        {
            // Retrieve data from database
            var dataArtwork = _dbSql.Artwork.Find(id);

            return (dataArtwork);
        }

        public Cart GetCartById(int id)
        {
            // Retrieve data from database
            var dataCart = _dbSql.Cart.Find(id);

            return (dataCart);
        }

        public CartItem GetCartItemById(int id)
        {
            // Retrieve data from database
            var dataCartItem = _dbSql.CartItem.Find(id);

            return (dataCartItem);
        }

        public IEnumerable<CartItem> GetUserCartItemByArtId(string userId, int artId)
        {
            // Retrieve data from database
            var dataCartItem = (from crt in _dbSql.Cart
                                join itm in _dbSql.CartItem
                                on crt.CartItemKey equals itm.CartItemKey
                                join art in _dbSql.Artwork
                                on itm.Key equals art.Key
                                where crt.UserKey == userId && art.Key == artId
                                select itm).AsNoTracking().ToList();  // Don't track (read-only); track if going to update right after.

            return (dataCartItem);
        }
        /*** - ***/


        /*** Insert ***/
        public void AddUserCartItem(string userId, int artId, int qty)
        {
            // Add cart item
            var newCartItem = new CartItem() { Key = artId, Qty = qty};
            var newCart = new Cart() { CartItem = newCartItem, UserKey = userId };
            _dbSql.Cart.Add(newCart);

            _dbSql.SaveChanges();
        }
        /*** - ***/


        /*** Update ***/
        public void UpdateUserCartItemQty(CartItem userCartItem)
        {
            // Update cart item
            _dbSql.CartItem.Update(userCartItem);

            _dbSql.SaveChanges();
        }
        /*** - ***/
    }
}
