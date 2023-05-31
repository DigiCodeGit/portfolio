using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.Identity.Data;
using Portfolio.Data;
using Portfolio.Data.Services;

namespace Portfolio.Controllers
{
    public class ECommerceController : Controller
    {
        /*** Using services to retrieve database instead of controller
        // private readonly PortalECommerceDbSql _dbSql;
        // Inject sql translator into constructor so controller can use it
        public ECommerceController (PortalECommerceDbSql dbSql)
        {
            _dbSql = dbSql;
        }
        ***/

        private readonly IEComService _comService;

        // Constructor - use to set service
        public ECommerceController(IEComService service)
        {
            _comService = service;
        }

        public IActionResult ECommerce()
        {
            /*** Get data using service
            // Retrieve data from database
            var dataArtwork = _dbSql.Artwork.ToList();
            ***/

            // Get data through service
            var dataArtWork = _comService.GetArt();

            // Pass the data to the view
            return View(dataArtWork);
        }
    }
}
