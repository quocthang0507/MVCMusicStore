using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCMusicStore.Controllers
{
	public class ControllerBase : Controller
	{
		private IMusicStoreEntities _storeDB;
		protected IMusicStoreEntities StoreDB { get { return _storeDB; } }

		public ControllerBase() : this(new MusicStoreEntities())
		{

		}

		public ControllerBase(IMusicStoreEntities musicStoreEntities)
		{
			this._storeDB = musicStoreEntities;
		}
	}
}