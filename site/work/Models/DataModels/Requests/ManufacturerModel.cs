namespace QFlow.Models.DataModels.Requests
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public ManufacturerModel Clone()
        {
            var clone = ( ManufacturerModel )MemberwiseClone();
            clone.Manufacturer = Manufacturer.Clone();
            return clone;
        }
    }
}
