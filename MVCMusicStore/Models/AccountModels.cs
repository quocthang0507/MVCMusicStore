using System.ComponentModel.DataAnnotations;

namespace MVCMusicStore.Models
{
	/// <summary>
	/// Thay đổi mật khẩu
	/// </summary>
	public class ChangePasswordModel
	{
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Mật khẩu hiện tại")]
		public string OldPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Mật khẩu {0} phải ít nhất {2} kí tự", MinimumLength = 7)]
		[DataType(DataType.Password)]
		[Display(Name = "Mật khẩu mới")]
		public string NewPassword { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Mật khẩu {0} phải ít nhất {2} kí tự", MinimumLength = 7)]
		[DataType(DataType.Password)]
		[Display(Name = "Xác nhận lại mật khẩu")]
		[System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp")]
		public string ConfirmPassword { get; set; }
	}

	/// <summary>
	/// Đăng nhập
	/// </summary>
	public class LogOnModel
	{
		[Required]
		[Display(Name = "Tên đăng nhập")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Mật khẩu")]
		public string Password { get; set; }

		[Display(Name = "Lưu mật khẩu?")]
		public bool RememberMe { get; set; }
	}

	/// <summary>
	/// Đăng ký tài khoản
	/// </summary>
	public class RegisterModel
	{
		[Required]
		[Display(Name = "Tên đăng nhập")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[Display(Name = "Địa chỉ email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "Mật khẩu {0} phải có ít nhất {2} kí tự", MinimumLength = 7)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Xác nhận lại mật khẩu")]
		[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
		public string ConfirmPassword { get; set; }
	}
}
