using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class TrackStep
    {
        public int Id { get; set; }
        [ForeignKey("Track")]
        public int TrackId { get; set; }
        public Track Track { get; set; }

        [ForeignKey("CurrentStep")]
        public int CurrentStepId { get; set; }
        public Step CurrentStep { get; set; }
        [ForeignKey("Currentstatus")]
        public int CurrentStatusId { get; set; }
        public Status Currentstatus {  set; get; }
        [ForeignKey("Action")]
        public int ActionId { get; set; }
        public Action Action { get; set; }
        [ForeignKey("NextStep")]
        public int NextStepId { get; set; }
        public Step NextStep { get; set; }

        [ForeignKey("Nextstatus")]
        public int NextStatusId { get; set; }
        public Status Nextstatus { set; get; }
        public bool IsActive { get; set; }

    }
}
