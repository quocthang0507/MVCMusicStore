using System.Data.Entity;
using System.Threading.Tasks;

namespace MVCMusicStore.Models
{
	/// <summary>
	/// MusicStoreEntities
	/// This class will represent the Entity Framework database context, and will handle our create, read, update, and
	/// delete operations for us
	/// </summary>
	public class MusicStoreEntities : DbContext, IMusicStoreEntities
	{
		public IDbSet<Album> Albums { get; set; }
		public IDbSet<Genre> Genres { get; set; }
		public IDbSet<Artist> Artists { get; set; }
		public IDbSet<Cart> Carts { get; set; }
		public IDbSet<Order> Orders { get; set; }
		public IDbSet<OrderDetail> OrderDetails { get; set; }

		public void SetModified(object target)
		{
			Entry(target).State = EntityState.Modified;
		}

		public Task<int> SaveChangesAsync()
		{
			return Task.Factory.StartNew(() => SaveChanges());
		}
	}
}