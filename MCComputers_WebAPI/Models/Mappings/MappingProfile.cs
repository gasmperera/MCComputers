using AutoMapper;
using MCComputers_WebAPI.Models;
using MCComputers_WebAPI.Models.DTOs;

namespace MCComputers_WebAPI.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Invoice, InvoiceDTO>()
                .ForMember(dest => dest.InvoiceItems, opt => opt.MapFrom(src => src.InvoiceItems));

            CreateMap<InvoiceItem, InvoiceItemDTO>();

            CreateMap<InvoiceCreateRequestDTO, Invoice>()
                .ForMember(dest => dest.InvoiceItems, opt => opt.MapFrom(src => src.InvoiceItemDTO))
                .ForMember(dest => dest.TransactionDate, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());

            CreateMap<InvoiceItemDTO, InvoiceItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.InvoiceId, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore());
        }
    }

}
