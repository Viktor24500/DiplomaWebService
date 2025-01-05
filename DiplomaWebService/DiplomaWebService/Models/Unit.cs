namespace DiplomaWebService.Models
{
    public class Unit
    {
        public Unit(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
