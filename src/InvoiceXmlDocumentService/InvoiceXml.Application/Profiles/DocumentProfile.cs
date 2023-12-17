using AutoMapper;
using InvoiceXml.Application.Features.Commands.CreateDocument;
using InvoiceXml.Application.Features.Queries.GetDocument;
using InvoiceXml.Application.Features.Queries.GetDocuments;
using InvoiceXml.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            //Source->Target
            CreateMap<Document, DocumentListItem>()
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderAbonentId))
                .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.ReceiverAbonentId));
            CreateMap<Document, DocumentItem>();
            CreateMap<Document, CreateDocumentCommand>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CreatedBy))
                .ReverseMap();
        }
    }
}
