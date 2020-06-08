using MVCMusicStore.Models;
using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
	public class ControllerBase : AsyncController
	{
		private IMusicStoreEntities _storeDB;

		protected IMusicStoreEntities StoreDB { get { return _storeDB; } }

		public ControllerBase() : this(new MusicStoreEntities()) { }

		public ControllerBase(IMusicStoreEntities storeDb)
		{
			_storeDB = storeDb;
		}
	}
}