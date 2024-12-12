namespace DiplomaWebService.Models.Types
{
    public class InvoiceType
    {
        public InvoiceType(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
