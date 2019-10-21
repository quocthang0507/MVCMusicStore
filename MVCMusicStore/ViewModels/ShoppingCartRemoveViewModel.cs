namespace MVCMusicStore.ViewModels
{
	/// <summary>
	/// Loại bỏ giỏ hàng
	/// </summary>
	public class ShoppingCartRemoveViewModel
	{
		public string Message { get; set; }
		public decimal CartTotal { get; set; }
		public int CartCount { get; set; }
		public int ItemCount { get; set; }
		public int DeleteId { get; set; }
	}
}