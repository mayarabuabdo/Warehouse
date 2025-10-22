using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Models;

namespace Warehouse.Data
{
    public class RequestTypeGroupStatusStepAction
    {
        public int Id { get; set; }
        [ForeignKey("RequestType")]
        public int RequestTypeId { get; set; }
        public RequestType  RequestType { get; set; }
        [ForeignKey("Step")]
        public int StepId { get; set; }
        public Step Step { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [ForeignKey("Action")]
        public int ActionId { get; set; }
        public Action Action { get; set; }

    }
}
