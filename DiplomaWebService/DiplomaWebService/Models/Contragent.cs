namespace DiplomaWebService.Models
{
    public class Contragent
    {
        public Contragent(int contragentId, int? parentId, string? parentName, string contragentName, bool isActive, string statusDescription)
        {
            ContragentId = contragentId;
            ParentId = parentId;
            ParentName = parentName;
            ContragentName = contragentName;
            IsActive = isActive;
            StatusDescription = statusDescription;
        }

        public int ContragentId { get; set; }
        public int? ParentId { get; set; }

        public string? ParentName { get; set; }
        public string ContragentName { get; set; }
        public bool IsActive { get; set; }
        public string StatusDescription { get; set; }
    }
}
