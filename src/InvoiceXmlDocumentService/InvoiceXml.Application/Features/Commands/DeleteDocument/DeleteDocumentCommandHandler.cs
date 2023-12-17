using AutoMapper;
using InvoiceXml.Application.Contracts.Persistence;
using InvoiceXml.Application.Exceptions;
using InvoiceXml.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Features.Commands.DeleteDocument
{
    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IDocumentRepo _documentRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDocumentCommandHandler> _logger;

        public DeleteDocumentCommandHandler(IDocumentRepo documentRepo, IMapper mapper, ILogger<DeleteDocumentCommandHandler> logger)
        {
            _documentRepo = documentRepo ?? throw new ArgumentNullException(nameof(documentRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var documentToDelete = await _documentRepo.GetByIdAsync(request.Id);
            if (documentToDelete == null)
            {
                throw new NotFoundException(nameof(Document), request.Id);
            }

            await _documentRepo.RemoveAsync(documentToDelete);

            _logger.LogInformation($"Document {documentToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
