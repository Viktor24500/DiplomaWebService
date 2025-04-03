namespace DiplomaWebService.Request.Contragent
{
	public class ContragentUpdateRequest
	{
		public int? ParentId { get; set; }
		public string ContragentName { get; set; }
		public bool IsActive { get; set; }
		public string? ContragentDescription { get; set; }
	}
}
