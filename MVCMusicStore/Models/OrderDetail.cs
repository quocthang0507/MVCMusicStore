﻿namespace MVCMusicStore.Models
{
	/// <summary>
	/// Chi tiết đơn hàng
	/// </summary>
	public class OrderDetail
	{
		public int OrderDetailId { get; set; }
		public int OrderId { get; set; }
		public int AlbumId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public virtual Album Album { get; set; }
		public virtual Order Order { get; set; }
	}
}