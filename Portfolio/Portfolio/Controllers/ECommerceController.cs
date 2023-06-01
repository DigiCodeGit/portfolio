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

        // Local vars
        private readonly IHttpContextAccessor _comHttpAccess; // Allows us to access Session from HTTP
        private readonly IEComService _comService;

        // Constructor - use to set service
        public ECommerceController(IEComService service, IHttpContextAccessor comHttpAccess)
        {
            _comService = service;
            _comHttpAccess = comHttpAccess;

            // Get Session Info
            if (_comHttpAccess.HttpContext.Session.GetString("SessionId") == null) // First run, no session yet
            {
                _comHttpAccess.HttpContext.Session.SetString("SessionId", Guid.NewGuid().ToString());
            }
 
            // Save session info to pass to whatever view controller going to
            ViewBag.sesId = _comHttpAccess.HttpContext.Session.GetString("SessionId"); 
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
