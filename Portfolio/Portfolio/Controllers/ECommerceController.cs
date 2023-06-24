using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        }

        // Viewbag cannot/won't be set inside constructor, can only be set after constructor is processed which is by
        // event executed. Hence, override executed event.
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            // Save session info to pass to whatever view controller going to
            ViewBag.sesId = GetSessionId();

            // Note web section we're in (for Home nav link)
            ViewBag.controller = "ecommerce";

            // Get all cart items
            var cartItemsQty = _comService.GetAllUserCartItems(GetSessionId());
            ViewBag.cartIconQty = cartItemsQty.Sum(x => x.Qty);
        }

        public IActionResult ECommerce()
        {
            // Get data through service
            var dataArtWork = _comService.GetAllArt();

            // Pass the data to the view
            return View(dataArtWork);
        }

        public IActionResult Cart()
        {
            // Get data through service
            var cartItemsDetail = _comService.GetAllUserCartItemsDetailed(GetSessionId());

            // Pass the data to the view
            return View(cartItemsDetail);
        }

        [HttpPost]
        public JsonResult AddToCart([FromBody] ArtAddInfo artJSON)
        {
            // Local vars
            bool addSuccess = false;  // Notes if was able to successfully add item
            IEnumerable<CartItem> userCartItemList;    // User's cart item list returned from sql
            string userSesId = "";    // User's session id

            float itemTotalPrice = 0; // Item's total price
            float cartSubTotal = 0;  // Cart subtotal
            int cartQty = 0;          // Cart quantity

            // Init
            userSesId = GetSessionId();

            // Valid data
            if (artJSON != null)
            {
                // Check if item was previously added already
                userCartItemList = _comService.GetUserCartItemByArtId(userSesId, artJSON.ObjArtwork.Key);

                // This art exist in user's cart already
                if (userCartItemList != null && userCartItemList.Any())
                {
                    // Update quantity
                    var userCartItem = userCartItemList.First();
                    userCartItem.DateTime = DateTime.Now;

                    switch (artJSON.Action)
                    {
                        case 0: // Add
                            userCartItem.Qty += artJSON.Qty;
                            break;

                        case 1: // Change
                            userCartItem.Qty = artJSON.Qty;
                            break;
                    }

                    _comService.UpdateUserCartItemQty(userCartItem);
                }
                else  // This art is being added the first time to user's cart
                {
                    _comService.AddUserCartItem(userSesId, artJSON.ObjArtwork.Key, artJSON.Qty);
                }

                // Get all cart items
                var userItems = _comService.GetAllUserCartItems(userSesId);

                // Calculate subtotal
                foreach (var item in userItems)
                {
                    // Cart total
                    cartSubTotal += item.Qty * item.Price;

                    // Total price for this item
                    if (item.Key == artJSON.ObjArtwork.Key)
                    {
                        itemTotalPrice = item.Qty * item.Price;
                    }
                }
                
                // Get quantity
                cartQty = userItems.Sum(x => x.Qty);

                // Note success
                addSuccess = true;
            }

            // Check if add was successful
            if (addSuccess)
            {
                return Json(new { status = "success", cartItems = cartQty, itemTotalCost = itemTotalPrice, subTotal = cartSubTotal.ToString("0.00") });
            }
            else
            {
                return Json(new { status = "fail" });
            }
        }

        [HttpPost]
        public JsonResult RemoveItemFromCart([FromBody] ArtAddInfo artJSON)
        {
            // Local vars
            bool addSuccess = false;  // Notes if was able to successfully add item
            IEnumerable<CartItem> userCartItemList;    // User's cart item list returned from sql
            string userSesId = "";    // User's session id

            int cartQty = 0;          // Cart quantity
            float cartSubTotal = 0;  // Cart subtotal

            // Init
            userSesId = GetSessionId();

            // Valid data
            if (artJSON != null)
            {
                // Check if item exist
                userCartItemList = _comService.GetUserCartItemByArtId(userSesId, artJSON.ObjArtwork.Key);

                // This art exist in user's cart 
                if (userCartItemList != null && userCartItemList.Any())
                {
                    // Remove item
                    var userCartItem = userCartItemList.First();

                    _comService.DeleteUserCartItem(userCartItem);

                    // Note success
                    addSuccess = true;

                    // Get all cart items
                    var userItems = _comService.GetAllUserCartItems(userSesId);

                    // Get quantity
                    cartQty = userItems.Sum(x => x.Qty);

                    // Calculate subtotal
                    foreach (var item in userItems)
                    {
                        // Cart total
                        cartSubTotal += item.Qty * item.Price;
                    }
                }
            }

            // Check if add was successful
            if (addSuccess)
            {
                return Json(new { status = "success", cartItems = cartQty, subTotal = cartSubTotal.ToString("0.00") });
            }
            else
            {
                return Json(new { status = "fail" });
            }
        }

        private string GetSessionId()
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
