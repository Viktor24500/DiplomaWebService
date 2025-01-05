namespace DiplomaWebService.Models
{
	public class Sector
	{
		public Sector(int id, string name, string shortSectorName)
		{
			Id = id;
			Name = name;
			ShortSectorName = shortSectorName;

		}
		public int Id { get; set; }
		public string Name { get; set; }

		public string ShortSectorName { get; set; }
	}
}
