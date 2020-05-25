using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcMusicStore.Services;
using MVCMusicStore.Controllers;
using MVCMusicStore.Models;
using MVCMusicStore.Test.Fakes;
using Rhino.Mocks;
using System.Web.Mvc;

namespace MVSMusicStore.Test
{
	[TestClass]
	public class AccountControllerTest
	{
		[TestMethod]
		public void LogOn_WithValidModelAndURL_RedirectsToSpecifiedURL()
		{
			AccountController controller = GetWiredUpAccountController();
			LogOnModel inputModel = new LogOnModel() { UserName = "admin", Password = "P@55w0rd", RememberMe = false };
			var result = controller.LogOn(inputModel, "/Checkout/") as RedirectResult;
			Assert.AreEqual("/Checkout/", result.Url);
		}

		private AccountController GetWiredUpAccountController(IMembershipService memSvc = null, IAuthenticationService authSvc = null, FakeDataStore store = null)
		{
			memSvc = memSvc ?? MockRepository.GenerateMock<IMembershipService>();
			memSvc.Stub(a => a.ValidateUser(Arg.Is("admin"), Arg.Is("P@55w0rd"))).Return(true);
			authSvc = authSvc ?? MockRepository.GenerateStub<IAuthenticationService>();
			return ControllerFactory.GetWiredUpController<AccountController>(s => new AccountController(s, authSvc, memSvc), store: store);
		}

		private void AssertRouteIsHome(RedirectToRouteResult result)
		{
			if (!result.RouteValues.ContainsValue("Index") || !result.RouteValues.ContainsValue("Home"))
			{
				throw new AssertFailedException("Route is not Index/Home");
			}
		}
	}
}
