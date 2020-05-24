using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCMusicStore.Controllers;
using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace UnitTest1_2
{
	/// <summary>
	/// Tên sản phẩm không hợp lệ
	/// </summary>
	[TestClass]
	public class TestCase1
	{
		private StoreManagerController controller = new StoreManagerController();

		[TestMethod]
		public void Test1()
		{
			Album data = new Album { Title = "~!@#$%^&*()_+{}:", Genre = new Genre { Name = "Rock" }, Price = 8.99M, Artist = new Artist { Name = "Men At Work" }, AlbumArtUrl = "/Content/Images/placeholder.gif" };
			ActionResult result = controller.Create(data);
			// Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
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
