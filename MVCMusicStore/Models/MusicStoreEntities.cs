using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Models
{
	public class MusicStoreEntities : DbContext
	{
		public DbSet<Album> Albums { get; set; }
		public DbSet<Genre> Genres { get; set; }
	}
}