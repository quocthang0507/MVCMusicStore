using MVCMusicStore.Models;
using MVCMusicStore.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
	public class ShoppingCartController : ControllerBase
	{
		public ShoppingCartController()
		{

		}

		public ShoppingCartController(IMusicStoreEntities storeDb) : base(storeDb) { }

		// GET: ShoppingCart
		public ActionResult Index()
		{
			var cart = ShoppingCart.GetCart(this.HttpContext, StoreDB);
			// Set up our ViewModel
			var viewModel = new ShoppingCartViewModel
			{
				CartItems = cart.GetCartItems(),
				CartTotal = cart.GetTotal()
			};
			// Return the view
			return View(viewModel);
		}

		//
		// GET: /Store/AddToCart/5
		public ActionResult AddToCart(int id, int quantity = 1)
		{
			// Retrieve the album from the database
			var addedAlbum = StoreDB.Albums.Single(album => album.AlbumId == id);
			// Get the current shopping cart
			var cart = ShoppingCart.GetCart(this.HttpContext, StoreDB);
			// Add it to the shopping cart
			cart.AddToCart(addedAlbum, quantity);
			// Go back to the main store page for more shopping
			return RedirectToAction("Index");
		}

		//
		// AJAX: /ShoppingCart/RemoveFromCart/5
		[HttpPost]
		public ActionResult RemoveFromCart(int id)
		{
			// Get the current shopping cart
			var cart = ShoppingCart.GetCart(this.HttpContext, StoreDB);
			// Get the name of the album to display confirmation
			string albumName = StoreDB.Carts.Single(item => item.RecordId == id).Album.Title;
			// Remove from cart
			int itemCount = cart.RemoveFromCart(id);
			// Display the confirmation message
			var results = new ShoppingCartRemoveViewModel
			{
				Message = Server.HtmlEncode(albumName) + " đã được gỡ bỏ thành công ra khỏi giỏ hàng của bạn",
				CartTotal = cart.GetTotal(),
				CartCount = cart.GetCount(),
				ItemCount = itemCount,
				DeleteId = id
			};
			return Json(results);
		}

		//
		// GET: /ShoppingCart/CartSummary
		[ChildActionOnly]
		public ActionResult CartSummary()
		{
			var cart = ShoppingCart.GetCart(this.HttpContext, StoreDB);
			ViewData["CartCount"] = cart.GetCount();
			return PartialView("CartSummary");
		}
	}
}