using MVCMusicStore.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
	public class StoreController : ControllerBase
	{
		public StoreController()
		{

		}

		public StoreController(IMusicStoreEntities storeDb) : base(storeDb) { }


		// GET: Store
		public ActionResult Index()
		{
			var genres = StoreDB.Genres.ToList();
			return View(genres);
		}

		//
		// GET: /Store/Browse?genre=Disco
		// Use the .Single() in LINQ to query for the appropriate Genre onject
		public ActionResult Browse(string genre)
		{
			// Retrieve Genre and its Associated Albums from database
			var genreModel = StoreDB.Genres.Include("Albums").Single(g => g.Name == genre);
			return View(genreModel);
		}

		//
		// GET: /Store/Details/5
		// ASP.NET MVC will automatically pass the URL segment to you as a parameter
		public ActionResult Details(int id)
		{
			var album = StoreDB.Albums.Find(id);
			return View(album);
		}

		//
		// GET: /Store/GenreMenu
		[ChildActionOnly]
		public ActionResult GenreMenu()
		{
			var genres = StoreDB.Genres.ToList();
			return PartialView(genres);
		}
	}
}