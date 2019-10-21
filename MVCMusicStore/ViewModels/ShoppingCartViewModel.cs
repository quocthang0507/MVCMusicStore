using MVCMusicStore.Models;
using System.Collections.Generic;

namespace MVCMusicStore.ViewModels
{
	/// <summary>
	/// Giỏ hàng
	/// </summary>
	public class ShoppingCartViewModel
	{
		public List<Cart> CartItems { get; set; }
		public decimal CartTotal { get; set; }
	}
}