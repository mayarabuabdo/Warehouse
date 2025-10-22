using Warehouse.Models;

namespace Warehouse.services
{
    public interface IWarehouseService
    {
        Task EditWarehouseInfo(WarehouseModel warehouseModel);
        Task<List<WarehouseModel>> GetDeactivedWarehouses();
        Task<int> NewWarehouse(WarehouseModel warehouseModel);
        Task ActiveWarehouse(int warehouseId);
        Task DeactiveWarehouse(int warehouseId);
        Task<List<WarehouseModel>> GetActivatedWarehouses();

    }
}