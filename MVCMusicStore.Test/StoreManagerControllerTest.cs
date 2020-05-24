using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCMusicStore.Controllers;
using MVCMusicStore.Models;
using System.Web.Mvc;

namespace MVSMusicStore.Test
{
	/// <summary>
	/// Tên sản phẩm không hợp lệ
	/// </summary>
	[TestClass]
	public class StoreManagerControllerTest
	{

		[TestMethod]
		public void Create_Invalid_Album()
		{
			StoreManagerController controller = new StoreManagerController(MusicStoreEntitiesFactory.GetEmpty());
			Album data = new Album { Title = "~!@#$%^&*()_+{}:", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			RedirectToRouteResult result = controller.Create(data) as RedirectToRouteResult;
			Assert.IsTrue(result.RouteValues.ContainsValue("Index"));
		}

	}
}
