﻿using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.Identity.Data;
using Portfolio.Data;
using Portfolio.Data.Services;
using Portfolio.Models;

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

            // Save session info to pass to whatever view controller going to
            ViewBag.sesId = getSessionId();
        }

        public IActionResult ECommerce()
        {
            /*** Get data using service
            // Retrieve data from database
            var dataArtwork = _dbSql.Artwork.ToList();
            ***/

            // Get data through service
            var dataArtWork = _comService.GetAllArt();

            // Pass the data to the view
            return View(dataArtWork);
        }

        [HttpPost]
        public JsonResult AddToCart([FromBody] ArtAddInfo artJSON)
        {
            // Local vars
            bool addSuccess = false;  // Notes if was able to successfully add item
            IEnumerable<CartItem> userCartItemList;    // User's cart item list returned from sql
            string userSesId = "";    // User's session id

            // Init
            userSesId = getSessionId();

            // Valid data
            if (artJSON != null)
            {
                // Find/Get art info from database
                //Artwork artPricing = _comService.GetArtById(artJSON.Key);

                // Check if item was previously added already
                userCartItemList = _comService.GetUserCartItemByArtId(userSesId, artJSON.ObjArtwork.Key);

                // This art exist in user's cart already
                if (userCartItemList != null && userCartItemList.Any())
                {
                    // Update quantity
                    var userCartItem = userCartItemList.First();
                    userCartItem.Qty = artJSON.Qty;

                    _comService.UpdateUserCartItemQty(userCartItem);
                }
                else  // This art is being added the first time to user's cart
                {
                    _comService.AddUserCartItem(userSesId, artJSON.ObjArtwork.Key, artJSON.Qty);
                }

                // Calculate subtotal items and prices

                // Note success
                addSuccess = true;
            }

            // Check if add was successful
            if (addSuccess)
            {
                return Json (new { status = "success", cartItems = 4, subTotal = 99});
            }
            else
            {
                return Json(new { status = "fail" });
            }
        }

        private string getSessionId()
        {
            // Get id
            if (_comHttpAccess.HttpContext.Session.GetString("SessionId") == null) // First run, no session yet
            {
                _comHttpAccess.HttpContext.Session.SetString("SessionId", Guid.NewGuid().ToString());
            }

            return _comHttpAccess.HttpContext.Session.GetString("SessionId");
        }
    }
}
