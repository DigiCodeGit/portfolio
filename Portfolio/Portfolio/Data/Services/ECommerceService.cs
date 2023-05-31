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

        public IEnumerable<Artwork> GetArt()
        {
            // Retrieve data from database
            var dataArtwork = _dbSql.Artwork.ToList();

            return (dataArtwork);
        }
    }
}
