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
	public class TestCase1
	{

		[TestMethod]
		public void Test1()
		{
			StoreManagerController controller = new StoreManagerController(MusicStoreEntitiesFactory.GetEmpty());
			Album data = new Album { Title = "~!@#$%^&*()_+{}:", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			ActionResult result = controller.Create(data);
			Assert.IsInstanceOfType(result, typeof(System.Web.Mvc.RedirectToRouteResult));
		}

		[TestMethod]
		public void Test2()
		{
		}

		[TestMethod]
		public void Test3()
		{
		}

		[TestMethod]
		public void Test4()
		{
		}

	}
}
