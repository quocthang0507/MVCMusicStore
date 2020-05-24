using MVCMusicStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMusicStore.Test.Fakes
{
	class FakeDataStore : IMusicStoreEntities
	{

		private List<object> _modifieds = new List<object>();
		private List<object> _saveds = new List<object>();

		public System.Data.Entity.IDbSet<Album> Albums { get; set; }

		public System.Data.Entity.IDbSet<Genre> Genres { get; set; }

		public System.Data.Entity.IDbSet<Artist> Artists { get; set; }

		public System.Data.Entity.IDbSet<Cart> Carts { get; set; }

		public System.Data.Entity.IDbSet<Order> Orders { get; set; }

		public System.Data.Entity.IDbSet<OrderDetail> OrderDetails { get; set; }

		public virtual int SaveChanges()
		{
			// not threadsafe, but we're not sharing these across tests so shouldn't be needed
			_saveds.AddRange(_modifieds);
			return _modifieds.RemoveAll(m => true);
		}

		public virtual void SetModified(object target)
		{
			_modifieds.Add(target);
		}

		public void Dispose()
		{

		}

		public static string SAMPLE_ALBUM_NAME_PREFIX = "Album #";

		public Album GenerateAndAddAlbum(int albumId, int artistId, int genreId, decimal price)
		{
			Album album = GenerateAlbum(albumId, artistId, genreId, price);
			Albums.Add(album);
			return album;
		}

		public static Album GenerateAlbum(int albumId, int artistId, int genreId, decimal price)
		{
			return new Album() { AlbumId = albumId, AlbumArtUrl = "N/A", ArtistId = artistId, GenreId = genreId, Price = price, Title = SAMPLE_ALBUM_NAME_PREFIX + albumId.ToString(), OrderDetails = new List<OrderDetail>() };
		}

		public void GenerateAndAddGenre(int genreId, string genreName = "")
		{
			Genres.Add(GenerateGenre(genreId, genreName));
		}

		public static Genre GenerateGenre(int genreId, string genreName = "")
		{
			if (string.IsNullOrEmpty(genreName)) genreName = "Genre #" + genreId.ToString();
			return new Genre() { GenreId = genreId, Name = genreName, Description = genreName + " Described" };
		}

		public void GenerateAndAddArtist(int artistId)
		{
			Artists.Add(GenerateArtist(artistId));
		}

		public static Artist GenerateArtist(int artistId)
		{
			return new Artist() { ArtistId = artistId, Name = "Artist #" + artistId.ToString() };
		}

		public bool SavedThis(object target)
		{
			return _saveds.Any(s => s == target);
		}

		public System.Threading.Tasks.Task<int> SaveChangesAsync()
		{
			return Task.Factory.StartNew<int>(() => SaveChanges());
		}
	}
}
