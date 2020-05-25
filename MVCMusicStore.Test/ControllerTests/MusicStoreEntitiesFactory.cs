using MVCMusicStore.Models;
using MVCMusicStore.Test.Fakes;

namespace MVSMusicStore.Test
{
	/// <summary>
	/// Lớp khởi tạo entities trống, dùng cho lớp FakeDataStore
	/// </summary>
	public class MusicStoreEntitiesFactory
	{
		/// <summary>
		/// Khởi tạo các thực thể trống
		/// </summary>
		/// <returns></returns>
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
