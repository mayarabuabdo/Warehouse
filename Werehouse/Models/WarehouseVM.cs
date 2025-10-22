namespace Warehouse.Models
{
    public class WarehouseVM
    {
        public WarehouseModel warehouse { get; set; }
        public List<City> cities { get; set; }
        public List<Country> countries { get; set; }
    }
}
