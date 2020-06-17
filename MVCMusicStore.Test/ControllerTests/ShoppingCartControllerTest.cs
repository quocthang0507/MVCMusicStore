using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCMusicStore.Controllers;
using MVCMusicStore.Test.Fakes;
using System.Web.Mvc;

namespace MVSMusicStore.Test.ControllerTests
{
	///////////////////////NGUYỄN THÀNH QUỐC////////////////////////////
	/// <summary>
	/// Kiểm tra chức năng giỏ hàng
	/// </summary>
	[TestClass]
	public class ShoppingCartControllerTest
	{
		[TestMethod]
		public void AddProductToCartInPositive()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(1);
			dataStore.GenerateAndAddAlbum(3, 1, 1, 10M);
			ShoppingCartController controller = ControllerFactory.GetWiredUpController((s) => new ShoppingCartController(s), store: dataStore);
			RedirectToRouteResult result = controller.AddToCart(3, 5) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
		}

		[TestMethod]
		public void AddProductToCartInNegative()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(1);
			dataStore.GenerateAndAddAlbum(2, 1, 1, 10M);
			ShoppingCartController controller = ControllerFactory.GetWiredUpController((s) => new ShoppingCartController(s), store: dataStore);
			RedirectToRouteResult result = controller.AddToCart(2, -5) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("InvalidRequest"));
		}

		[TestMethod]
		public void AddProductToCartWhichExist()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(1);
			dataStore.GenerateAndAddAlbum(3, 1, 1, 10M);
			ShoppingCartController controller = ControllerFactory.GetWiredUpController((s) => new ShoppingCartController(s), store: dataStore);
			controller.AddToCart(3, 5);
			RedirectToRouteResult result = controller.AddToCart(3, 5) as RedirectToRouteResult;
			controller.CartSummary();
			var cartCount = controller.ViewData["CartCount"].ToString();
			Assert.IsTrue(cartCount == "10");
		}

		[TestMethod]
		public void RemoveProductToCartWhichExist()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(1);
			dataStore.GenerateAndAddAlbum(3, 1, 1, 10);
			ShoppingCartController controller = ControllerFactory.GetWiredUpController((s) => new ShoppingCartController(s), store: dataStore);
			controller.AddToCart(3, 5);
			RedirectToRouteResult result = controller.RemoveFromCart(0) as RedirectToRouteResult;
			controller.CartSummary();
			var cartCount = controller.ViewData["CartCount"].ToString();
			Assert.IsTrue(cartCount == "4");
		}
	}
}
