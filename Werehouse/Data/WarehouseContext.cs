using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
namespace Warehouse.Data
{
    public class WarehouseContext : IdentityDbContext<AppUser>
    {
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) { }
        public DbSet<CountryDto> countries { get; set; }
        public DbSet<CityDTO> cities { get; set; }
        public DbSet<WarehouseDTO> warehouses { get; set; }
        public DbSet<WareHouseItemsRelation> wareHouseItemsRelation { get; set; }
        public DbSet<WarehouseItemsDTO> WarehouseItemsDTO {  get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<RequestDTO> Requests { get; set; }
        public DbSet<RequestTypeStepStatusGroup> RequestTypeStepStatusGroup { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Step> Step { get; set; }
        public DbSet<Track> Track { get; set; }
        public DbSet<TrackStep> TrackStep { get; set; }
        public DbSet<RequestTypeGroupStatusStepAction> RequestTypeGroupStatusStepAction { get; set; }
        public DbSet<Document> Document {  get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<RequestTypeStepStatusGroupDocumentAction> RequestTypeStepStatusGroupDocumentAction { get; set; }
        public DbSet<WarehouseItemRequestDetails> WarehouseItemRequestDetails { get; set; }
        public DbSet<RequestDocumentLog> RequestDocumentLog { get; set; }
        public DbSet<RequestLog> RequestLog { get; set; }

    }
}
