namespace DiplomaWebService.Parametrs.Units
{
	public class UnitCreateParameters
	{
		public UnitCreateParameters(string name, string? description)
		{
			Name = name;
			Description = description;
		}
		public string Name { get; set; }

		public string? Description { get; set; }
	}
}
