using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVCMusicStore.Models
{
	/// <summary>
	/// Đơn hàng
	/// </summary>
	[Bind(Exclude = "OrderId")]
	public partial class Order
	{
		[ScaffoldColumn(false)]
		public int OrderId { get; set; }

		[ScaffoldColumn(false)]
		public System.DateTime OrderDate { get; set; }

		[ScaffoldColumn(false)]
		public string Username { get; set; }

		[Required(ErrorMessage = "Họ là bắt buộc")]
		[DisplayName("Họ và tên lót")]
		[StringLength(160)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Tên là bắt buộc")]
		[DisplayName("Tên")]
		[StringLength(160)]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Số nhà và tên đường là bắt buộc")]
		[DisplayName("Số nhà, tên đường")]
		[StringLength(100)]
		public string Address { get; set; }

		[Required(ErrorMessage = "Quận huyện là bắt buộc")]
		[DisplayName("Quận huyện")]
		[StringLength(40)]
		public string District { get; set; }

		[Required(ErrorMessage = "Tỉnh thành là bắt buộc")]
		[DisplayName("Tỉnh thành")]
		[StringLength(40)]
		public string Province { get; set; }

		[Required(ErrorMessage = "Mã bưu điện là bắt buộc")]
		[DisplayName("Mã bưu điện")]
		[StringLength(10)]
		public string PostalCode { get; set; }

		[Required(ErrorMessage = "Tên nước là bắt buộc")]
		[DisplayName("Quốc gia")]
		[StringLength(40)]
		public string Country { get; set; }

		[Required(ErrorMessage = "Số điện thoại là bắt buộc")]
		[DisplayName("Số điện thoại")]
		[StringLength(24)]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Địa chỉ email là bắt buộc")]
		[DisplayName("Email")]
		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email không hợp lệ")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[ScaffoldColumn(false)]
		public decimal Total { get; set; }

		public List<OrderDetail> OrderDetails { get; set; }
	}
}
