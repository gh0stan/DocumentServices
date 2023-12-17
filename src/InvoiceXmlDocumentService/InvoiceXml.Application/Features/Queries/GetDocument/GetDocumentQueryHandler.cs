using AutoMapper;
using InvoiceXml.Application.Contracts.Persistence;
using InvoiceXml.Application.Features.Queries.GetDocuments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Features.Queries.GetDocument
{
    public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentQuery, DocumentItem>
    {
        private readonly IDocumentRepo _documentRepo;
        private readonly IMapper _mapper;

        public GetDocumentsQueryHandler(IDocumentRepo documentRepository, IMapper mapper)
        {
            _documentRepo = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DocumentItem> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            var document = await _documentRepo.GetDocumentByGuidAsync(request.DocumentGuid);
            return _mapper.Map<DocumentItem>(document);
        }
    }
}
