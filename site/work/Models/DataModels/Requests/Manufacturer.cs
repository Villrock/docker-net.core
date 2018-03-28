namespace QFlow.Models.DataModels.Requests
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Manufacturer Clone()
        {
            return ( Manufacturer )MemberwiseClone();
        }
    }
}
