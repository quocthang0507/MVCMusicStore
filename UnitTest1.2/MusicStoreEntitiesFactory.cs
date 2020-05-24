using MVCMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Telerik.JustMock;
using Telerik.JustMock.Core;

namespace UnitTest1_2
{
	public class MusicStoreEntitiesFactory
	{
		public static IMusicStoreEntities GetEmpty()
		{
			return Mock.Create<IMusicStoreEntities>();
		}
	}
}
