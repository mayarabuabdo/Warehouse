using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Data;

namespace Warehouse.Models
{
    public class RequestLogModel
    {
        public int Id { get; set; }

        public int RequestId { get; set; }
        public string RequestName { get; set; }

        public string ActionTokenById { get; set; }
        public string ActionTokenByName { get; set; }
        public string ActionTokenAt { get; set; }
  
        public int StepId { get; set; }
        public string StepName { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public int ActionId { get; set; }
        public string ActionName { get; set; }  
    }
}
