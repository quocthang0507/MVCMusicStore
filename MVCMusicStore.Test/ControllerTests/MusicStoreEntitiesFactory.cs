using MVCMusicStore.Models;
using MVCMusicStore.Test.Fakes;

namespace MVSMusicStore.Test
{
	/// <summary>
	/// Khởi tạo entities trống, dùng cho lớp FakeDataStore
	/// </summary>
	public class MusicStoreEntitiesFactory
	{
		public static FakeDataStore GetEmpty()
		{
			FakeDataStore datastore = new FakeDataStore();
			datastore.Albums = new FakeDbSet<Album>();
			datastore.Artists = new FakeDbSet<Artist>();
			datastore.Carts = new FakeDbSet<Cart>();
			datastore.Genres = new FakeDbSet<Genre>();
			datastore.OrderDetails = new FakeDbSet<OrderDetail>();
			datastore.Orders = new FakeDbSet<Order>();
			return datastore;
		}
	}
}
