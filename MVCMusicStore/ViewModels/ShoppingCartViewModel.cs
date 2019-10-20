using MVCMusicStore.Models;
using System.Collections.Generic;

namespace MVCMusicStore.ViewModels
{
	public class ShoppingCartViewModel
	{
		public List<Cart> CartItems { get; set; }
		public decimal CartTotal { get; set; }
	}
}