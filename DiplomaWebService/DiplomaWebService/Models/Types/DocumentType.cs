namespace DiplomaWebService.Models.Types
{
    public class DocumentType
    {
        public DocumentType(int id, string name, string? shortName)
        {
            Id = id;
            Name = name;
            ShortName = shortName;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string? ShortName { get; set; }
    }
}
