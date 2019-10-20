using MVCMusicStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
	public class HomeController : Controller
	{
		MusicStoreEntities storeDB = new MusicStoreEntities();

		// GET: Home
		public ActionResult Index()
		{
			// Get most popular albums
			var albums = GetTopSellingAlbums(5);
			return View(albums);
		}

		private List<Album> GetTopSellingAlbums(int count)
		{
			// Group the order details by album and return
			// the albums with the highest count
			return storeDB.Albums
			.OrderByDescending(a => a.OrderDetails.Count())
			.Take(count)
			.ToList();
		}
	}
}