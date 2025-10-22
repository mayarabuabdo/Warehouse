using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class RequestDocumentLog
    {
        public int Id { get; set; }
     
        [ForeignKey("Request")]
        public int RequestId { get; set; }
        public RequestDTO Request { get; set; } 

        [ForeignKey("Step")]
        public int StepId { get; set; }
        public Step Step { get; set; } 

        [ForeignKey("Status")]
        public int StatusId {  get; set; }
        public Status Status { get; set; } 
        public string Extension { get; set; }

        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        [ForeignKey("document")]
        public int DocumentId { get; set; }
        public Document document { get; set; } 

    }
}
