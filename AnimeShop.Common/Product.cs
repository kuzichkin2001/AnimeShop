using System;
namespace AnimeShop.Common
{
	public class Product
	{
		public int Id { get; set; }
		public ProductType ProductType { get; set; }
		public string Name { get; set; }
		public int Amount { get; set; }
		public bool Seasonal { get; set; }
	}
}

