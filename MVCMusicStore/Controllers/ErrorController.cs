using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
	public class ErrorController : Controller
	{
		// GET: Error
		public ViewResult Index()
		{
			return View("Error");
		}

		// GET: NotFound
		public ViewResult NotFound()
		{
			Response.StatusCode = 404;  //you may want to set this to 200
			return View("NotFound");
		}
        public ViewResult InvalidRequest()
        {
            Response.StatusCode = 405;
            return View();
        }
	}
}