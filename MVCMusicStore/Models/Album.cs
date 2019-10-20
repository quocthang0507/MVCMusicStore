using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMusicStore.Models
{
	public class Album
	{
		public int AlbumId { get; set; }
		public int GenreId { get; set; }
		public int ArtistId { get; set; }
		public string Title { get; set; }
		public decimal Price { get; set; }
		public string AlbumArtUrl { get; set; }
		public Genre Genre { get; set; }
		public Artist Artist { get; set; }
	}
}