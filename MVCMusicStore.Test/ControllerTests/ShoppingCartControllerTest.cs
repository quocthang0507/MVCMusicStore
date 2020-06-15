using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCMusicStore.Controllers;
using MVCMusicStore.Test.Fakes;
using System.Web.Mvc;

namespace MVSMusicStore.Test.ControllerTests
{
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
            dataStore.GenerateAndAddArtist(10);
            dataStore.GenerateAndAddAlbum(3, 1, 1, 10M);
            ShoppingCartController controller = ControllerFactory.GetWiredUpController<ShoppingCartController>((s) => new ShoppingCartController(s), store: dataStore);
            //Album album = new Album { Title = "Tình khúc vượt thời gian", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
            //controller.AddToCart(1, 5);
            RedirectToRouteResult result = controller.AddToCart(3, 5) as RedirectToRouteResult;
            Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
        }
        [TestMethod]
        public void AddProductToCartInNegative()
        {
            FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
            dataStore.GenerateAndAddGenre(1);
            dataStore.GenerateAndAddArtist(10);
            dataStore.GenerateAndAddAlbum(2, 1, 1, 10M);
            ShoppingCartController controller = ControllerFactory.GetWiredUpController<ShoppingCartController>((s) => new ShoppingCartController(s), store: dataStore);
            //Album album = new Album { Title = "Tình khúc vượt thời gian", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
            //controller.AddToCart(1, 5);
            RedirectToRouteResult result = controller.AddToCart(2, -5) as RedirectToRouteResult;
            Assert.IsTrue(result.RouteValues.ContainsValue("InvalidRequest"));
        }
        [TestMethod]
        public void AddProductToCartWhichExist()
        {
            FakeDataStore dataStore = MusicStoreEntitiesFactory.GetEmpty();
            dataStore.GenerateAndAddGenre(1);
            dataStore.GenerateAndAddArtist(10);
            dataStore.GenerateAndAddAlbum(3, 1, 1, 10M);
            ShoppingCartController controller = ControllerFactory.GetWiredUpController<ShoppingCartController>((s) => new ShoppingCartController(s), store: dataStore);
            //Album album = new Album { Title = "Tình khúc vượt thời gian", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
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
            dataStore.GenerateAndAddArtist(10);
            dataStore.GenerateAndAddAlbum(3, 1, 1, 10);
            ShoppingCartController controller = ControllerFactory.GetWiredUpController<ShoppingCartController>((s) => new ShoppingCartController(s), store: dataStore);
            //Album album = new Album { Title = "Tình khúc vượt thời gian", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
            controller.AddToCart(3, 5);
            RedirectToRouteResult result = controller.RemoveFromCart(3) as RedirectToRouteResult;
            controller.CartSummary();
            var cartCount = controller.ViewData["CartCount"].ToString();
            Assert.IsTrue(cartCount == "4");
        }
    }
}
