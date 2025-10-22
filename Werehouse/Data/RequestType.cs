using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Data
{
    public class RequestType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Track")]
        public int TrackId { get; set; }
        public Track Track { get; set; }
        public bool IsActive { get; set; }
    }
}
