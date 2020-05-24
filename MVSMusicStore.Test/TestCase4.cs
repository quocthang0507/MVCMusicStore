using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVCMusicStore.Controllers;
using MVCMusicStore.Models;
using System.Web.Mvc;

namespace MVSMusicStore.Test
{
	/// <summary>
	/// Giá sản phẩm hợp lệ
	/// </summary>
	[TestClass]
	public class TestCase4
	{
		private StoreManagerController controller = new StoreManagerController();
		private Album data1 = new Album();

		[TestMethod]
		public void Test1()
		{
			ActionResult result = controller.Create();
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
