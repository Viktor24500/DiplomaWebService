namespace DiplomaWebService.Parametrs.Sector
{
    public class SectorCreateParameters
    {
        public SectorCreateParameters(string name, string shortSectorName)
        {
            Name = name;
            ShortName = shortSectorName;

        }
        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}
