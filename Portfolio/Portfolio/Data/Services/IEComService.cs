using Portfolio.Models;

// Interface only
namespace Portfolio.Data.Services
{
    public interface IEComService
    {
        IEnumerable<Artwork> GetArt();
    }
}
