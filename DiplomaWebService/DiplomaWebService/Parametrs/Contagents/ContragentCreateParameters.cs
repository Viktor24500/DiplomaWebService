namespace DiplomaWebService.Parametrs.Contagents
{
    public class ContragentCreateParameters
    {
        public ContragentCreateParameters(int? parentId, string contragentName, bool isActive)
        {
            ParentId = parentId;
            ContragentName = contragentName;
            IsActive = isActive;

        }

        public int? ParentId { get; set; }

        public string ContragentName { get; set; }
        public bool IsActive { get; set; }
    }
}
