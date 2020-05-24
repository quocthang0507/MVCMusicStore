using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMusicStore.Models
{
	/// <summary>
	/// Giỏ hàng
	/// </summary>
	public class ShoppingCart
	{
		private IMusicStoreEntities storeDB;
		private string ShoppingCartId { get; set; }

		public const string CartSessionKey = "CartId";

		public ShoppingCart(IMusicStoreEntities dbContext)
		{
			storeDB = dbContext;
		}

		/// <summary>
		/// Lấy giỏ hàng từ user
		/// </summary>
		/// <param name="context">HTTP Request</param>
		/// <returns>Giỏ hàng</returns>
		public static ShoppingCart GetCart(HttpContextBase context, IMusicStoreEntities dbContext)
		{
			var cart = new ShoppingCart(dbContext);
			cart.ShoppingCartId = cart.GetCartId(context);
			return cart;
		}

		/// <summary>
		/// Helper method to simplify shopping cart calls
		/// </summary>
		/// <param name="controller"></param>
		/// <returns></returns>
		public static ShoppingCart GetCart(Controller controller, IMusicStoreEntities dbContext)
		{
			return GetCart(controller.HttpContext, dbContext);
		}

		/// <summary>
		/// Thêm một album vào giỏ hàng
		/// </summary>
		/// <param name="album">Album cần thêm</param>
		public void AddToCart(Album album)
		{
			AddToCart(album, 1);
		}

		/// <summary>
		/// Thêm một album vào giỏ hàng cùng với số lượng
		/// </summary>
		/// <param name="album">Album cần thêm</param>
		/// <param name="quantity">Số lượng</param>
		public void AddToCart(Album album, int quantity)
		{
			if (quantity <= 0)
				return;
			// Get the matching cart and album instances
			var cartItem = storeDB.Carts.SingleOrDefault(c => c.CartId == ShoppingCartId && c.AlbumId == album.AlbumId);
			if (cartItem == null)
			{
				// Create a new cart item if no cart item exists
				cartItem = new Cart
				{
					AlbumId = album.AlbumId,
					CartId = ShoppingCartId,
					Count = quantity,
					DateCreated = DateTime.Now
				};
				storeDB.Carts.Add(cartItem);
			}
			else
			{
				// If the item exists in the cart, then add the number to the quantity
				cartItem.Count += quantity;
			}
			// Save changes
			storeDB.SaveChanges();
		}


		/// <summary>
		/// Bỏ một đơn hàng ra khỏi giỏ hàng
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public int RemoveFromCart(int id)
		{
			// Get the cart
			var cartItem = storeDB.Carts.Single(cart => cart.CartId == ShoppingCartId && cart.RecordId == id);
			int itemCount = 0;
			// If it exists
			if (cartItem != null)
			{
				if (cartItem.Count > 1)
				{
					cartItem.Count--;
					itemCount = cartItem.Count;
				}
				else
				{
					storeDB.Carts.Remove(cartItem);
				}
				// Save changes
				storeDB.SaveChanges();
			}
			return itemCount;
		}

		/// <summary>
		/// Làm trống giỏ hàng
		/// </summary>
		public void EmptyCart()
		{
			var cartItems = storeDB.Carts.Where(cart => cart.CartId == ShoppingCartId);
			foreach (var cartItem in cartItems)
			{
				storeDB.Carts.Remove(cartItem);
			}
			// Save changes
			storeDB.SaveChanges();
		}

		/// <summary>
		/// Hiển thị danh sách các đơn hàng có trong giỏ hàng
		/// </summary>
		/// <returns></returns>
		public List<Cart> GetCartItems()
		{
			return storeDB.Carts.Where(cart => cart.CartId == ShoppingCartId).ToList();
		}

		/// <summary>
		/// Trả về số lượng giỏ hàng
		/// </summary>
		/// <returns>Số lượng</returns>
		public int GetCount()
		{
			// Get the count of each item in the cart and sum them up
			int? count = (from cartItems in storeDB.Carts
						  where cartItems.CartId == ShoppingCartId
						  select (int?)cartItems.Count).Sum();
			// Return 0 if all entries are null
			return count ?? 0;
		}

		/// <summary>
		/// Tính tổng tiền giỏ hàng
		/// </summary>
		/// <returns>Tổng tiền</returns>
		public decimal GetTotal()
		{
			// Multiply album price by count of that album to get
			// the current price for each of those albums in the cart
			// sum all album price totals to get the cart total
			decimal? total = (from cartItems in storeDB.Carts
							  where cartItems.CartId == ShoppingCartId
							  select (int?)cartItems.Count * cartItems.Album.Price).Sum();
			return total ?? decimal.Zero;
		}

		/// <summary>
		/// Tạo một đơn hàng
		/// </summary>
		/// <param name="order"></param>
		/// <returns>ID đơn hàng</returns>
		public int CreateOrder(Order order)
		{
			decimal orderTotal = 0;
			var cartItems = GetCartItems();
			// Iterate over the items in the cart, adding the order details for each
			foreach (var item in cartItems)
			{
				var orderDetail = new OrderDetail
				{
					AlbumId = item.AlbumId,
					OrderId = order.OrderId,
					UnitPrice = item.Album.Price,
					Quantity = item.Count
				};
				// Set the order total of the shopping cart
				orderTotal += (item.Count * item.Album.Price);
				storeDB.OrderDetails.Add(orderDetail);
			}
			// Set the order's total to the orderTotal count
			order.Total = orderTotal;
			// Save the order
			storeDB.SaveChanges();
			// Empty the shopping cart
			EmptyCart();
			// Return the OrderId as the confirmation number
			return order.OrderId;
		}

		/// <summary>
		/// We're using HttpContextBase to allow access to cookies.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public string GetCartId(HttpContextBase context)
		{
			if (context.Session[CartSessionKey] == null)
			{
				if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
					context.Session[CartSessionKey] = context.User.Identity.Name;
				else
				{
					// Generate a new random GUID using System.Guid class
					Guid tempCartId = Guid.NewGuid();
					// Send tempCartId back to client as a cookie
					context.Session[CartSessionKey] = tempCartId.ToString();
				}
			}
			return context.Session[CartSessionKey].ToString();
		}

		/// <summary>
		/// When a user has logged in, migrate their shopping cart to
		/// be associated with their username
		/// </summary>
		/// <param name="userName"></param>
		public void MigrateCart(string userName)
		{
			var shoppingCart = storeDB.Carts.Where(c => c.CartId == ShoppingCartId);
			foreach (Cart item in shoppingCart)
				item.CartId = userName;
			storeDB.SaveChanges();
		}
	}
}