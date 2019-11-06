using System.Data.Entity;

namespace MVCMusicStore.Models
{
	/// <summary>
	/// MusicStoreEntities
	/// This class will represent the Entity Framework database context, and will handle our create, read, update, and
	/// delete operations for us
	/// </summary>
	public class MusicStoreEntities : DbContext
	{
		public DbSet<Album> Albums { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
	}
}