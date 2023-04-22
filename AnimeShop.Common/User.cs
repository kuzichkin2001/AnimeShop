using System;
namespace AnimeShop.Common
{
	public class User
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public long ChatId { get; set; }
		public bool AccountActivated { get; set; }
	}
}

