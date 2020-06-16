using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCMusicStore.Controllers;
using MVCMusicStore.Models;
using MVCMusicStore.Test.Fakes;
using System.Web.Mvc;

namespace MVSMusicStore.Test
{
	///////////////////////////LA QUỐC THẮNG//////////////////////////////
	/// <summary>
	/// Kiểm tra chức năng thêm và cập nhật sản phẩm
	/// </summary>
	[TestClass]
	public class StoreManagerControllerTest
	{

		/// <summary>
		/// /////////////////////////// CREATE /////////////////////////////////////
		/// </summary>
		[TestMethod]
		public void Create_Invalid_Name_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			StoreManagerController controller = ControllerFactory.GetWiredUpController((s) => new StoreManagerController(s), store: dataStore);
			Album album = new Album { Title = "          ", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			RedirectToRouteResult result = controller.Create(album) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("InvalidRequest"));
		}

		[TestMethod]
		public void Create_Valid_Name_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			StoreManagerController controller = ControllerFactory.GetWiredUpController((s) => new StoreManagerController(s), store: dataStore);
			Album album = new Album { Title = "Tình khúc vượt thời gian", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			RedirectToRouteResult result = controller.Create(album) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
		}

		[TestMethod]
		public void Create_Invalid_Price_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
			Album album = new Album { Title = "Tình khúc vượt thời gian", Genre = new Genre { Name = "Rock" }, Price = -100M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			RedirectToRouteResult result = controller.Create(album) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("InvalidRequest"));
		}

		[TestMethod]
		public void Create_Miss_Required_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
			Album album = new Album { Genre = new Genre { Name = "Rock" }, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			RedirectToRouteResult result = controller.Create(album) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("InvalidRequest"));
		}

		/// <summary>
		/// /////////////////////////// EDIT /////////////////////////////////////
		/// </summary>

		[TestMethod]
		public void Edit_NotExsiting_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
			Album album = new Album { Title = "Tình khúc vượt thời gian", Genre = new Genre { Name = "Rock" }, Price = 10M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			RedirectToRouteResult result = controller.Edit(album) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("InvalidRequest"));
		}

		[TestMethod]
		public void Edit_Valid_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1, "Nhe nhang");
			dataStore.GenerateAndAddArtist(10);
			// Generate a new album before updating
			Album test = dataStore.GenerateAndAddAlbum(100, 10, 1, 19.99M);
			StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
			test.Title = "Tình khúc vượt thời gian";
			test.Price = 10M;
			RedirectToRouteResult result = controller.Edit(test) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
		}

		[TestMethod]
		public void Edit_Invalid_Price_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			Album test = dataStore.GenerateAndAddAlbum(100, 10, 1, 19.99M);
			StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
			test.Title = "Tình khúc vượt thời gian";
			test.Price = -10M;
			RedirectToRouteResult result = controller.Edit(test) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("InvalidRequest"));
		}

		[TestMethod]
		public void Edit_Miss_Required_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			Album test = dataStore.GenerateAndAddAlbum(100, 10, 1, 19.99M);
			StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
			test.Title = null;
			RedirectToRouteResult result = controller.Edit(test) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("InvalidRequest"));
		}
	}
}
