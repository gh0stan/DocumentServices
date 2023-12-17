using AutoMapper;
using InvoiceXml.Application.Contracts.Infrastructure;
using InvoiceXml.Application.Contracts.Persistence;
using InvoiceXml.Application.Email;
using InvoiceXml.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InvoiceXml.Application.Features.Commands.CreateDocument
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, int>
    {
        private readonly IUserRepo _userRepo;
        private readonly IDocumentRepo _documentRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateDocumentCommandHandler> _logger;

        public CreateDocumentCommandHandler(IDocumentRepo documentRepo,
            IUserRepo userRepo,
            IMapper mapper,
            IEmailService emailService,
            ILogger<CreateDocumentCommandHandler> logger)
        {
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _documentRepo = documentRepo ?? throw new ArgumentNullException(nameof(documentRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var documentEntity = _mapper.Map<Document>(request);
            var newDocument = await _documentRepo.AddAsync(documentEntity);
            
            _logger.LogInformation($"Document {newDocument.Id} is successfully created.");

            await SendMail(newDocument);

            return newDocument.Id;
        }

        private async Task SendMail(Document document)
        {
            var user = await _userRepo.GetByIdAsync(document.CreatedBy);

            var email = new EmailDto() { To = user.Email, Body = $"Document {document.Title} was created.", Subject = "Document created." };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email. Document: {document.Id}, User: {user.Id}. {ex.Message}.");
            }
        }
    }
}
