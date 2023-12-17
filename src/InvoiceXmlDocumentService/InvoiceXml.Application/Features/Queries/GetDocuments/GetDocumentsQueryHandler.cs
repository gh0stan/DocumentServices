using AutoMapper;
using InvoiceXml.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Features.Queries.GetDocuments
{
    public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, List<DocumentListItem>>
    {
        private readonly IDocumentRepo _documentRepo;
        private readonly IMapper _mapper;

        public GetDocumentsQueryHandler(IDocumentRepo documentRepository, IMapper mapper)
        {
            _documentRepo = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<DocumentListItem>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
        {
            var documentList = await _documentRepo.GetDocumentsByAbonentAsync(request.AbonentId);
            var docs = _mapper.Map<List<DocumentListItem>>(documentList);
            
            return docs;
        }
    }
}
