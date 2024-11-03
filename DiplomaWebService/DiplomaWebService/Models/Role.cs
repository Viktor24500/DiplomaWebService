namespace DiplomaWebService.Models
{
    public class Role
    {
        public Role(int id, string name, string shortSectorName)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
