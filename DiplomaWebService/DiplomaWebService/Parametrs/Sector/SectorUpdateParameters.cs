namespace DiplomaWebService.Parametrs.Sector
{
	public class SectorUpdateParameters
	{
		public SectorUpdateParameters(int id, string name, string shortName)
		{
			Id = id;
			Name = name;
			ShortName = shortName;

		}
		public int Id { get; set; }
		public string Name { get; set; }

		public string ShortName { get; set; }
	}
}
