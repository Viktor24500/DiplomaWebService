namespace DiplomaWebService.Parametrs.Units
{
    public class UnitUpdateParameters
    {
        public UnitUpdateParameters(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
