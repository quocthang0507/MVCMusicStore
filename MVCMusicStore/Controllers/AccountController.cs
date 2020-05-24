using MVCMusicStore.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCMusicStore.Controllers
{
	public class AccountController : ControllerBase
	{
		private string CookieName = "UserCookies";

		public AccountController()
		{

		}
		
		public AccountController(IMusicStoreEntities storeDb) : base(storeDb) { }

		private void MigrateShoppingCart(string UserName)
		{
			// Associate shopping cart items with logged-in user
			var cart = ShoppingCart.GetCart(this.HttpContext, StoreDB);
			cart.MigrateCart(UserName);
			Session[ShoppingCart.CartSessionKey] = UserName;
		}

		//
		// GET: /Account/LogOn
		public ActionResult LogOn()
		{
			return View();
		}

		//
		// POST: /Account/LogOn
		[HttpPost]
		public ActionResult LogOn(LogOnModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (Membership.ValidateUser(model.UserName, model.Password))
				{
					MigrateShoppingCart(model.UserName);

					FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
					if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
					{
						CreateCookie(model.UserName);
						return Redirect(returnUrl);
					}
					else
					{
						CreateCookie(model.UserName);
						return RedirectToAction("Index", "Home");
					}
				}
				else
					ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu!");
			}
			// If we got this far, something failed, redisplay form
			return View(model);
		}

		//
		// GET: /Account/LogOff
		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			MakeCookieExpried();
			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// Tạo cookie lưu tên người dùng
		/// </summary>
		/// <param name="username">Tên người dùng</param>
		private void CreateCookie(string username)
		{
			var cookies = new HttpCookie(CookieName);
			cookies.Value = username;
			cookies.Expires = DateTime.Now.AddHours(1);
			Response.Cookies.Add(cookies);
		}

		/// <summary>
		/// Làm cookie hết hạn
		/// </summary>
		private void MakeCookieExpried()
		{
			if (Request.Cookies[CookieName] != null)
			{
				var cookies = new HttpCookie(CookieName);
				cookies.Value = "";
				cookies.Expires = DateTime.Now.AddDays(-1);
				Response.Cookies.Add(cookies);
			}
		}

		//
		// GET: /Account/Register
		public ActionResult Register()
		{
			return View();
		}

		//
		// POST: /Account/Register
		[HttpPost]
		public ActionResult Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				// Attempt to register the user
				MembershipCreateStatus createStatus;
				Membership.CreateUser(model.UserName, model.Password, model.Email, "question", "answer", true, null, out createStatus);
				if (createStatus == MembershipCreateStatus.Success)
				{
					MigrateShoppingCart(model.UserName);
					FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
					return RedirectToAction("Index", "Home");
				}
				else
					ModelState.AddModelError("", ErrorCodeToString(createStatus));
			}
			// If we got this far, something failed, redisplay form
			return View(model);
		}

		//
		// GET: /Account/ChangePassword
		[Authorize]
		public ActionResult ChangePassword()
		{
			return View();
		}

		//
		// POST: /Account/ChangePassword
		[Authorize]
		[HttpPost]
		public ActionResult ChangePassword(ChangePasswordModel model)
		{
			if (ModelState.IsValid)
			{
				// ChangePassword will throw an exception rather than return false in certain failure scenarios.
				bool changePasswordSucceeded;
				try
				{
					MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
					changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
				}
				catch (ArgumentException)
				{
					changePasswordSucceeded = false;
					ModelState.AddModelError("", "Mật khẩu cũ/mới không hợp lệ");

				}
				if (changePasswordSucceeded)
					return RedirectToAction("ChangePasswordSuccess");
			}
			// If we got this far, something failed, redisplay form
			return View(model);
		}

		//
		// GET: /Account/ChangePasswordSuccess
		public ActionResult ChangePasswordSuccess()
		{
			return View();
		}

		#region Status Codes
		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// See http://go.microsoft.com/fwlink/?LinkID=177550 for a full list of status codes.
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "Tên đăng nhập đã tồn tại trong hệ thống, vui lòng đổi sang tên khác";
				case MembershipCreateStatus.InvalidUserName:
					return "Tên đăng nhập không hợp lệ, vui lòng sử dụng tên đăng nhập khác";
				case MembershipCreateStatus.DuplicateEmail:
					return "Email đã tồn tại trong hệ thống, vui lòng sử dụng email khác";
				case MembershipCreateStatus.InvalidEmail:
					return "Email không hợp lệ, vui lòng sử dụng email khác";
				case MembershipCreateStatus.InvalidPassword:
					return "Mật khẩu không hợp lệ, vui lòng sử dụng mật khẩu khác";
				case MembershipCreateStatus.InvalidAnswer:
					return "Câu trả lời không hợp lệ, vui lòng sử dụng câu trả lời khác";
				case MembershipCreateStatus.InvalidQuestion:
					return "Câu hỏi không hợp lệ, vui lòng sử dụng câu hỏi khác";
				case MembershipCreateStatus.ProviderError:
					return "Lỗi chức thực, vui lòng thử lại. Nếu vẫn còn tiếp diễn, thì không biết phải làm sao!";
				case MembershipCreateStatus.UserRejected:
					return "Yêu cầu từ người dùng bị từ chối, vui lòng thử lại. Nếu vẫn còn tiếp diễn, thì không biết phải làm sao!";
				default:
					return "Lỗi phát sinh không xác định. Nếu vẫn còn tiếp diễn, thì không biết phải làm sao!";
			}
		}
		#endregion
	}
}
