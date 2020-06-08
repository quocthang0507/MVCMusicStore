using MVCMusicStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
	public class HomeController : ControllerBase
	{
		public HomeController()
		{

		}

		public HomeController(IMusicStoreEntities storeDb) : base(storeDb) { }

		// GET: Home
		public ActionResult Index()
		{
			// Get most popular albums
			var albums = GetTopSellingAlbums(20);
			return View(albums);
		}

		private List<Album> GetTopSellingAlbums(int count)
		{
			// Group the order details by album and return
			// the albums with the highest count
			return StoreDB.Albums.OrderByDescending(a => a.OrderDetails.Count()).Take(count).ToList();
		}

		public ActionResult Contact()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}
	}
}