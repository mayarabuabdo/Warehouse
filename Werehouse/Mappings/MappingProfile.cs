using AutoMapper;
using Warehouse.Data;
using Warehouse.Models;
using Warehouse.services;
using Warehouse.services;
namespace Warehouse.Mappings
{
    public class MappingProfile : Profile
    {
        public string GetFileIcon(string path)
        {
            var ext = Path.GetExtension(path)?.TrimStart('.').ToLower() ?? "unknown";
            return ext switch
            {
                "jpg" or "jpeg" or "png" or "gif" => "image",
                "pdf" => "pdf.png",
                "xls" or "xlsx" => "excel.png",
                "doc" or "docx" => "word.png",
                "ppt" or "pptx" => "powerpoint.png",
                "txt" => "text.png",
                "zip" or "rar" => "archive.png",
                _ => "file"
            };
        }

        public MappingProfile() {

            CreateMap<RequestDocumentLog, RequestDocumentLogModel>()
           .ForMember(dest => dest.StepName, opt => opt.MapFrom(src => src.Step.Name))
           .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name)).
            ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.document.Name)).
         
            ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedBy.UserName)).
            ForMember(dest => dest.FileExtentionType, opt => opt.MapFrom(src => GetFileIcon(src.Extension))
            );
          
            CreateMap<RequestDocumentLogModel, RequestDocumentLog>()
           .ForMember(dest => dest.Step, opt => opt.Ignore())
           .ForMember(dest => dest.Status, opt => opt.Ignore())
           .ForMember(dest => dest.document, opt => opt.Ignore())
           .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            CreateMap<Warehouse.Data.Action, ActionModelcs>();


            CreateMap<RequestLog, RequestLogModel>()
          .ForMember(dest => dest.StepName, opt => opt.MapFrom(src => src.Step.Name))
          .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.ActionName, opt => opt.MapFrom(src => src.Action.Name)).
            ForMember(dest => dest.ActionTokenByName, opt => opt.MapFrom(src => src.ActionTokenBy.UserName));


            CreateMap<RequestLogModel, RequestLog>()
           .ForMember(dest => dest.Step, opt => opt.Ignore())
           .ForMember(dest => dest.Status, opt => opt.Ignore()).
           ForMember(dest => dest.Action, opt => opt.Ignore()).
           ForMember(dest => dest.ActionTokenBy, opt => opt.Ignore());
        

        }
    }

}
