namespace DiplomaWebService.Parametrs.Contagents
{
    public class ContragentUpdateParameters
    {
        public ContragentUpdateParameters(int contragentId, int? parentId, string contragentName, bool isActive)
        {
            ContragentId = contragentId;
            ParentId = parentId;
            ContragentName = contragentName;
            IsActive = isActive;

        }

        public int ContragentId { get; set; }
        public int? ParentId { get; set; }

        public string ContragentName { get; set; }
        public bool IsActive { get; set; }
    }
}
