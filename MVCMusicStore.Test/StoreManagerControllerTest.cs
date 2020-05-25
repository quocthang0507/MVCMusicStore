using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCMusicStore.Controllers;
using MVCMusicStore.Models;
using MVCMusicStore.Test.Fakes;
using System.Web.Mvc;

namespace MVSMusicStore.Test
{
	/// <summary>
	/// Kiểm tra chức năng thêm sản phẩm
	/// </summary>
	[TestClass]
	public class StoreManagerControllerTest
	{

		[TestMethod]
		public void Create_Invalid_Name_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
			Album album = new Album { Title = "~!@#$%^&*()_+{}:", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			RedirectToRouteResult result = controller.Create(album) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
		}

		[TestMethod]
		public void Create_Valid_Name_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
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
			Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
		}

		[TestMethod]
		public void Create_Valid_Price_Album()
		{
			FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			dataStore.GenerateAndAddGenre(1);
			dataStore.GenerateAndAddArtist(10);
			StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
			Album album = new Album { Title = "Tình khúc vượt thời gian", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			RedirectToRouteResult result = controller.Create(album) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
		}

		[TestMethod]
		public void Edit_Invalid_Name_Album()
		{
			//FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
			//dataStore.GenerateAndAddGenre(1);
			//dataStore.GenerateAndAddArtist(10);
			//Album testAlbum = dataStore.GenerateAndAddAlbum(100, 10, 1, 19.99m);
			//StoreManagerController controller = ControllerFactory.GetWiredUpController<StoreManagerController>((s) => new StoreManagerController(s), store: dataStore);
			//ViewResult result = controller.Edit(testAlbum.AlbumId) as ViewResult;
			//Album model = (Album)result.Model;
			//Assert.AreEqual(testAlbum, model);
		}
	}
}
