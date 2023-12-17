using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Features.Commands.CreateDocument
{
    public class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
    {
        public CreateDocumentCommandValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{Title} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{Title} must not exceed 100 characters.");

            RuleFor(p => p.InvoiceTotal)
                .NotEmpty().WithMessage("{InvoiceTotal} is required.")
                .GreaterThan(0).WithMessage("{InvoiceTotal} should be greater than zero.");

            RuleFor(p => p.DocumentXml)
                .NotEmpty()
                .WithMessage("Document is missing.");
            
            RuleFor(p => p.FileName)
                .NotEmpty();

            RuleFor(p => p.Guid)
                .NotEmpty();
            
            RuleFor(p => p.UserId)
                .NotEmpty();
            
            RuleFor(p => p.ReceiverAbonentId)
                .NotEmpty();
            
            RuleFor(p => p.SenderAbonentId)
                .NotEmpty();
        }
    }
}
