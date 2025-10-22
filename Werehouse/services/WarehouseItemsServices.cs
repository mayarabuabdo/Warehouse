using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Data;
using Warehouse.Models;
namespace Warehouse.services
{

    public class WarehouseItemsServices : IWarehouseItemsServices
    {
        WarehouseContext warehouseContext;

        public WarehouseItemsServices(WarehouseContext _warehouseContext)
        {
            warehouseContext = _warehouseContext;

        }
        public async Task<List<WarehouseItems>> GetActivatedItems()
        {
            
          List<WareHouseItemsRelation> WareHouseItemsRelations = warehouseContext.wareHouseItemsRelation.Include(w => w.warehouseItemsDTO).Include(w => w.warehouseDTO).Where(w=>w.warehouseItemsDTO.IsActive==true).ToList();
            List<WarehouseItems> Items = new List<WarehouseItems>();

            if(WareHouseItemsRelations != null) {
                foreach (WareHouseItemsRelation wareHouseItemsRelation in WareHouseItemsRelations)
                {
                    WarehouseItems warehouseItems = new WarehouseItems()
                    {
                        Name = wareHouseItemsRelation.warehouseItemsDTO.Name,
                        CostPrice = wareHouseItemsRelation.warehouseItemsDTO.CostPrice,
                        KUCode = wareHouseItemsRelation.warehouseItemsDTO.KUCode,
                        MSRPPrice = wareHouseItemsRelation.warehouseItemsDTO.MSRPPrice,
                        Qty = wareHouseItemsRelation.warehouseItemsDTO.Qty,
                        WarehouseId = wareHouseItemsRelation.warehouseDTO.Id,
                        WarehouseName = wareHouseItemsRelation.warehouseDTO.Name,
                        Id= wareHouseItemsRelation.ItemId,
                        IsWarehouseActive=wareHouseItemsRelation.warehouseDTO.IsActive

                    };
                    Items.Add(warehouseItems);
                }
            }
          

            return Items;
        }
        public async Task<List<WarehouseItems>> GetDeactivatedItems()
        {

            List<WareHouseItemsRelation> WareHouseItemsRelations = warehouseContext.wareHouseItemsRelation.Include(w => w.warehouseItemsDTO).Include(w => w.warehouseDTO).Where(w=>w.warehouseItemsDTO.IsActive==false).ToList();
            List<WarehouseItems> Items = new List<WarehouseItems>();

            if (WareHouseItemsRelations != null)
            {
                foreach (WareHouseItemsRelation wareHouseItemsRelation in WareHouseItemsRelations)
                {
                    WarehouseItems warehouseItems = new WarehouseItems()
                    {
                        Name = wareHouseItemsRelation.warehouseItemsDTO.Name,
                        CostPrice = wareHouseItemsRelation.warehouseItemsDTO.CostPrice,
                        KUCode = wareHouseItemsRelation.warehouseItemsDTO.KUCode,
                        MSRPPrice = wareHouseItemsRelation.warehouseItemsDTO.MSRPPrice,
                        Qty = wareHouseItemsRelation.warehouseItemsDTO.Qty,
                        WarehouseId = wareHouseItemsRelation.warehouseDTO.Id,
                        WarehouseName = wareHouseItemsRelation.warehouseDTO.Name,
                        Id = wareHouseItemsRelation.ItemId,
                        IsWarehouseActive=wareHouseItemsRelation.warehouseDTO.IsActive

                    };
                    Items.Add(warehouseItems);
                }
            }


            return Items;
        }
           public async Task AddItems(WarehouseItems warehouseItem,string userName)
            {
       
            if( warehouseItem != null) {
  bool isWarehouseExist = warehouseContext.WarehouseItemsDTO.Any(w => w.Name == warehouseItem.Name);

                if (!isWarehouseExist)
                {

                    int RequestTypeId = warehouseContext.RequestTypes.Where(r => r.Name == "Add new WarehouseItem").Select(r => r.Id).FirstOrDefault();
                    int StepId = warehouseContext.Step.Where(r => r.Name == "add WarehouseItem").Select(r => r.Id).FirstOrDefault();
                    int StausId = warehouseContext.Status.Where(r => r.Name == "Pending").Select(r => r.Id).FirstOrDefault();
                    AppUser CreatedBy = warehouseContext.Users.Where(r => r.UserName == userName).FirstOrDefault();
                    string CreatedById = warehouseContext.Users.Where(r => r.UserName == userName).Select(u => u.Id).FirstOrDefault();
                    int ActionId = warehouseContext.Actions.Where(r => r.Name == "Submit").Select(u => u.Id).FirstOrDefault();
                    string createdTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                    RequestDTO request = new RequestDTO()
                    {
                        RequestTypeId = RequestTypeId,
                        StepId = StepId,
                        StausId = StausId,
                        CreatedBy = CreatedBy,
                        CreatedById = CreatedById,
                        CreatedAt = createdTime,
                    };

                    warehouseContext.Requests.Add(request);
                    warehouseContext.SaveChanges();

                 
                    WarehouseItemRequestDetails warehouseItemRequestDetails = new WarehouseItemRequestDetails()
                    {
                        KUCode = warehouseItem.KUCode,
                        CostPrice = warehouseItem.CostPrice,
                        Name = warehouseItem.Name,
                        MSRPPrice = warehouseItem.MSRPPrice,
                        WarehouseId = warehouseItem.WarehouseId,
                        Qty = warehouseItem.Qty,

                        RequestId = request.Id,
                    };

                    warehouseContext.WarehouseItemRequestDetails.Add(warehouseItemRequestDetails);
                    warehouseContext.SaveChanges();
                    int createActionId= warehouseContext.Actions.Where(a => a.Name == "Create").Select(a=>a.Id).FirstOrDefault();

                    RequestLog requestLog = new RequestLog()
                    {
                      RequestId= request.Id,
                      StepId=request.StepId,
                      StatusId=request.StausId,
                      ActionTokenAt= createdTime,
                      ActionTokenById=request.CreatedById,
                      ActionId= createActionId


                    };
                    warehouseContext.RequestLog.Add(requestLog);
                    warehouseContext.SaveChanges();

                }
            }
           

             
            }

        public async Task AddWarehouseItem(WarehouseItems warehouseItem)
        {

        }

        public async Task DeactiveWarehouseItem(int warehouseItemId)
        {
            WarehouseItemsDTO warehouseItems = warehouseContext.WarehouseItemsDTO.FirstOrDefault(w=>w.Id== warehouseItemId);
            if(warehouseItems != null)
            {
                warehouseItems.IsActive = false;
                warehouseContext.SaveChanges();
            }
        }
        public async Task ActiveWarehouseItem(int warehouseItemId)
        {
            WareHouseItemsRelation WareHouseItemsRelations = warehouseContext.wareHouseItemsRelation.Include(w => w.warehouseItemsDTO).Include(w => w.warehouseDTO).FirstOrDefault(w => w.warehouseItemsDTO.Id == warehouseItemId);
            WarehouseItemsDTO warehouseItems = WareHouseItemsRelations.warehouseItemsDTO;
            if (warehouseItems != null && WareHouseItemsRelations.warehouseDTO.IsActive==true)
            {
                
                warehouseItems.IsActive = true;
                warehouseContext.SaveChanges();
            }
        }
        public async Task EditWarehouseItem(WarehouseItems warehouseItem)
        {
            WarehouseItemsDTO warehouseItems = warehouseContext.WarehouseItemsDTO.FirstOrDefault(w => w.Id == warehouseItem.Id);

            if (warehouseItems != null) {
                warehouseItems.Name = warehouseItem.Name;
                warehouseItems.CostPrice = warehouseItem.CostPrice;
                warehouseItems.MSRPPrice= warehouseItem.MSRPPrice;
                warehouseItems.Qty= warehouseItem.Qty;
                warehouseItems.KUCode= warehouseItem.KUCode;
                WareHouseItemsRelation wareHouseItemsRelationToUpdate= warehouseContext.wareHouseItemsRelation.FirstOrDefault(w => w.ItemId == warehouseItem.Id);
                wareHouseItemsRelationToUpdate.WarehouseId= warehouseItem.WarehouseId;
                warehouseContext.SaveChanges();
            }
        }
    
    }
}
