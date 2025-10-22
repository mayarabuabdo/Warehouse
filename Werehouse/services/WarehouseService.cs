using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.Data;
namespace Warehouse.services
{
    public class WarehouseService : IWarehouseService
    {
        WarehouseContext warehouseContext;
        ICityServices cityServices;
        public WarehouseService(WarehouseContext _warehouseContext, ICityServices _cityServices)
        {
            warehouseContext = _warehouseContext;
            cityServices = _cityServices;
        }

        public async Task<List<WarehouseModel>> GetActivatedWarehouses()
        {


            List<WarehouseDTO> warehouses = warehouseContext.warehouses.Include(w=>w.City).Include(c=>c.Country).Where(c => c.IsActive == true && c.City.IsActive == true && c.Country.IsActive == true).ToList();
            List<WarehouseModel> warehouseModels = new List<WarehouseModel>();
            if (warehouses != null)
            {
                foreach (var warehouse in warehouses)
                {
                    WarehouseModel warehouseModel = new WarehouseModel()
                    {
                        CountryId = warehouse.CountryId,
                        CityId = warehouse.CityId,
                        Name = warehouse.Name,
                        Description = warehouse.Description,
                        CountryName=warehouse.Country.Name,
                        CityName=warehouse.City.Name,
                        Id=warehouse.Id
                    };
                    warehouseModels.Add(warehouseModel);
                }
            }
            return warehouseModels;
        }
        public async Task<List<WarehouseModel>> GetDeactivedWarehouses()
        {


            List<WarehouseDTO> warehouses = warehouseContext.warehouses.Include(w => w.City).Include(c => c.Country).Where(c => c.IsActive == false || c.City.IsActive==false || c.Country.IsActive==false).ToList();
            List<WarehouseModel> warehouseModels = new List<WarehouseModel>();
            if (warehouses != null)
            {
                foreach (var warehouse in warehouses)
                {
                    WarehouseModel warehouseModel = new WarehouseModel()
                    {
                        CountryId = warehouse.CountryId,
                        CityId = warehouse.CityId,
                        Name = warehouse.Name,
                        Description = warehouse.Description,
                        CountryName = warehouse.Country.Name,
                        CityName = warehouse.City.Name,
                        Id = warehouse.Id
                    };
                    warehouseModels.Add(warehouseModel);
                }
            }
            return warehouseModels;
        }
        public async Task<int> NewWarehouse(WarehouseModel warehouseModel)
        {
            if (warehouseModel != null)
            {

                bool isWarehouseExist = warehouseContext.warehouses.Any(w => w.Name == warehouseModel.Name);
                if (isWarehouseExist)
                {

                    return 1;
                }
                else
                {

                    WarehouseDTO newWarehouse = new WarehouseDTO()
                    {
                        Description = warehouseModel.Description,
                        Id = warehouseModel.Id,
                        CityId = warehouseModel.CityId,
                        CountryId = warehouseModel.CountryId,
                        Name = warehouseModel.Name,
                        IsActive=true
                    };


                    warehouseContext.warehouses.Add(newWarehouse);
                    warehouseContext.SaveChanges();
                    return 2;

                }

            }

            else
            {
                return 3;
            }

        }

        public async Task DeactiveWarehouse(int warehouseId)
        {
            WarehouseDTO warehouse = warehouseContext.warehouses.Include(w=>w.warehouseItemsDTO).FirstOrDefault(w => w.Id == warehouseId);
            if (warehouse != null)
            {
                foreach (var item in warehouse.warehouseItemsDTO)
                {
                    item.IsActive = false;
                }
                warehouse.IsActive = false;
                warehouseContext.SaveChanges();
            }
        }
        public async Task ActiveWarehouse(int warehouseId)
        {
            WarehouseDTO warehouse = warehouseContext.warehouses.Include(w=>w.City).Include(w=>w.Country).FirstOrDefault(w => w.Id == warehouseId);
            if (warehouse != null && warehouse.City.IsActive==true && warehouse.Country.IsActive==true)
            {
               
                warehouse.IsActive = true;
                warehouseContext.SaveChanges();
            }
        }

        public async Task EditWarehouseInfo(WarehouseModel warehouseModel)
        {
            WarehouseDTO warehouseToEdit = warehouseContext.warehouses.FirstOrDefault(w => w.Id == warehouseModel.Id);

            if (warehouseToEdit != null)
            {
                warehouseToEdit.Description = warehouseModel.Description;
                warehouseToEdit.CityId = warehouseModel.CityId;
                warehouseToEdit.CountryId = warehouseModel.CountryId;
                warehouseToEdit.Name = warehouseModel.Name;

                warehouseContext.SaveChanges();

            }

        }



    }
}
