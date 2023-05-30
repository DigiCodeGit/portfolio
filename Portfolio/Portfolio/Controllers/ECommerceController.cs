using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.Identity.Data;

namespace Portfolio.Controllers
{
    public class ECommerceController : Controller
    {
        private readonly PortalECommerceDbSql _dbSql;

        // Inject sql translator into constructor so controller can use it
        public ECommerceController (PortalECommerceDbSql dbSql)
        {
            _dbSql = dbSql;
        }

        public IActionResult ECommerce()
        {
            // Retrieve data from database
            var dataArtwork = _dbSql.Artwork.ToList();

            // Pass the data to the view
            return View(dataArtwork);
        }
    }
}
