using Microsoft.EntityFrameworkCore;
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
        CartItem GetUserCartItemByArtId(int userId, int artId)
        {
            // Retrieve data from database
            var dataCartItem = _dbSql.Cart
                .Where(crt => crt.UserKey == userId)
                .Include(itm => itm.CartItem)
                    .Where(itm => itm.key);

            return (dataCartItem);
        }
        
    }
}
