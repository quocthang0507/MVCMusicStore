using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
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

		DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
		int SaveChanges();
	}
}
