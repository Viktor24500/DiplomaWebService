namespace DiplomaWebService.Parametrs.Contagents
{
	public class ContragentCreateParameters
	{
		public ContragentCreateParameters(int? parentId, string contragentName, bool isActive, string? contragentDescription, bool whoAmI)
		{
			ParentId = parentId;
			ContragentName = contragentName;
			IsActive = isActive;
			ContragentDescription = contragentDescription;
			WhoAmI = whoAmI;
		}

		public int? ParentId { get; set; }

		public string ContragentName { get; set; }
		public bool IsActive { get; set; }
		public string? ContragentDescription { get; set; }
		public bool WhoAmI { get; set; } = false;
	}
}
