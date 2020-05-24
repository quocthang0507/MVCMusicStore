using MVCMusicStore.Models;
using Rhino.Mocks;

namespace MVSMusicStore.Test
{
	public class MusicStoreEntitiesFactory
	{
		public static IMusicStoreEntities GetEmpty()
		{
			return MockRepository.GenerateMock<IMusicStoreEntities>();
		}
	}
}
