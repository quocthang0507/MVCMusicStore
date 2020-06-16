using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMusicStore.Services;
using MVCMusicStore.Controllers;
using MVCMusicStore.Models;
using MVCMusicStore.Test.Fakes;
using Rhino.Mocks;
using System.Web.Mvc;

namespace MVSMusicStore.Test
{
	////////////////////////////TRẦN TRỌNG HIỆP//////////////////////////////
	/// <summary>
	/// Kiểm tra chức năng đăng nhập
	/// </summary>
	[TestClass]
	public class AccountControllerTest
	{
		[TestMethod]
		public void LogOn_Valid_Login()
		{
			AccountController controller = GetWiredUpAccountController();
			LogOnModel inputModel = new LogOnModel() { UserName = "admin", Password = "P@55w0rd", RememberMe = false };
			var result = controller.LogOn(inputModel, "/Checkout/") as RedirectResult;
			Assert.AreEqual("/Checkout/", result.Url);
		}

		private AccountController GetWiredUpAccountController(IMembershipService membershipSvc = null, IAuthenticationService authSvc = null, FakeDataStore store = null)
		{
			membershipSvc = membershipSvc ?? MockRepository.GenerateMock<IMembershipService>();
			membershipSvc.Stub(a => a.ValidateUser(Arg.Is("admin"), Arg.Is("P@55w0rd"))).Return(true); // Khoi tao thong tin dang nhap tu truoc
			authSvc = authSvc ?? MockRepository.GenerateStub<IAuthenticationService>();
			return ControllerFactory.GetWiredUpController<AccountController>(s => new AccountController(s, authSvc, membershipSvc), store: store);
		}
	}
}
