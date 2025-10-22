using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Data;

namespace Warehouse.Models
{
  
    public class RequestDocumentLogModel
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int StepId { get; set; }
        public string StepName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Extension { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedAt { get; set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public IFormFile DocumentFile { get; set; }
        public string FileExtentionType { get; set; }

    }
}
