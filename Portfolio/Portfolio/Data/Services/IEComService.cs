using Portfolio.Models;

// Interface only
namespace Portfolio.Data.Services
{
    public interface IEComService
    {
        // Artwork
        IEnumerable<Artwork> GetAllArt();
        Artwork GetArtById(int id);

        // Cart
        Cart GetCartById(int id);

        // User cart
        Cart GetUserCartItemByArtId(int userId, int artId);

    }
}
