using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TryWeb.Models
{
	public class Aspects
	{
		[Key]
		public string Name { get; set; }
		public int Score { get; set; }
		public Categories Category { get; set; }
		public enum Categories { Movie, Serial, Animation, Comix, Videogame, Book, Album }
		public bool IsWatched { get; set; }
	}
}