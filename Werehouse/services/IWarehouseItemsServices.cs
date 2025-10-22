using Warehouse.Models;

namespace Warehouse.services
{
    public interface IWarehouseItemsServices
    {
        Task AddItems(WarehouseItems warehouseItem, string userName);
        Task<List<WarehouseItems>> GetActivatedItems();
        Task<List<WarehouseItems>> GetDeactivatedItems();
        Task DeactiveWarehouseItem(int warehouseItemId);
        Task ActiveWarehouseItem(int warehouseItemId);
        Task EditWarehouseItem(WarehouseItems warehouseItem);
    }
}