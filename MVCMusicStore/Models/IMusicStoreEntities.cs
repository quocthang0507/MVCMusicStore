using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace MVCMusicStore.Models
{
	public interface IMusicStoreEntities : IDisposable
	{
		IDbSet<Album> Albums { get; set; }
		IDbSet<Genre> Genres { get; set; }
		IDbSet<Artist> Artists { get; set; }
		IDbSet<Cart> Carts { get; set; }
		IDbSet<Order> Orders { get; set; }
		IDbSet<OrderDetail> OrderDetails { get; set; }

		int SaveChanges();
		void SetModified(object target);

		Task<int> SaveChangesAsync();
	}
}
