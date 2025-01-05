namespace DiplomaWebService.Parametrs.Role
{
    public class RoleUpdateParametrs
    {
        public RoleUpdateParametrs(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
