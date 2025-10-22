using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class RequestTypeStepStatusGroup
    {
        public int Id { get; set; }
        [ForeignKey("Step")]
        public int StepId { get; set; }
        public Step Step { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [ForeignKey("Request")]
        public int RequestTypeId { get; set; }
        public RequestType Request { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }  
    }
}
