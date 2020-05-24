using MVCMusicStore.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
	[Authorize]
	public class CheckoutController : ControllerBase
	{
		private const string PromoCode = "FREE";

		public CheckoutController()
		{

		}

		public CheckoutController(IMusicStoreEntities storeDb) : base(storeDb) { }

		//
		// GET: /Checkout/AddressAndPayment
		public ActionResult AddressAndPayment()
		{
			return View();
		}

		//
		// POST: /Checkout/AddressAndPayment
		[HttpPost]
		public ActionResult AddressAndPayment(FormCollection values)
		{
			var order = new Order();
			TryUpdateModel(order);
			try
			{
				if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
					return View(order);
				else
				{
					order.Username = User.Identity.Name;
					order.OrderDate = DateTime.Now;
					//Save Order
					StoreDB.Orders.Add(order);
					StoreDB.SaveChanges();
					//Process the order
					var cart = ShoppingCart.GetCart(this.HttpContext, StoreDB);
					cart.CreateOrder(order);
					return RedirectToAction("Complete", new { id = order.OrderId });
				}
			}
			catch
			{
				//Invalid - redisplay with errors
				return View(order);
			}
		}

		//
		// GET: /Checkout/Complete
		public ActionResult Complete(int id)
		{
			// Validate customer owns this order
			bool isValid = StoreDB.Orders.Any(o => o.OrderId == id && o.Username == User.Identity.Name);
			if (isValid)
				return View(id);
			else
				return View("Error");
		}
	}
}