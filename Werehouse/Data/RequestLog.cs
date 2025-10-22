using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class RequestLog
    {
        public int Id { get; set; }
        [ForeignKey("Request")]
        public int RequestId { get; set; }
        public RequestDTO Request { get; set; }
        [ForeignKey("ActionTokenBy")]
        public string ActionTokenById { get; set; }
        public AppUser ActionTokenBy { get; set; }
        public string ActionTokenAt { get; set; }
        [ForeignKey("Step")]
        public int StepId { get; set; }
        public Step Step { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [ForeignKey("Action")]
        public int ActionId { get; set; }
        public Action Action { get; set; }  
    }
}
