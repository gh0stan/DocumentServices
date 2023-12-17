using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using InvoiceXml.Application.Features.Queries.GetDocuments;
using InvoiceXml.Application.Features.Commands.DeleteDocument;
using InvoiceXml.Application.Features.Commands.CreateDocument;
using InvoiceXml.Application.Features.Queries.GetDocument;

namespace InvoiceXml.API.Controllers
{
    [ApiController]
    [Route("api/x/[controller]/[action]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{abonentId}", Name = "GetDocuments")]
        [ProducesResponseType(typeof(IEnumerable<DocumentListItem>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DocumentListItem>>> GetDocuments(int abonentId)
        {
            var query = new GetDocumentsQuery(abonentId);
            var documents = await _mediator.Send(query);
            return Ok(documents);
        }

        [HttpGet("{guid}", Name = "GetDocumentByGuid")]
        [ProducesResponseType(typeof(DocumentListItem), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DocumentListItem>> GetDocumentByGuid(string guid)
        {
            var query = new GetDocumentQuery(guid);
            var document = await _mediator.Send(query);
            return Ok(document);
        }

        // testing purpose
        [HttpPost(Name = "CreateDocument")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateDocument([FromBody] CreateDocumentCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute(nameof(GetDocumentByGuid), new { command.Guid });
        }

        [HttpDelete("{id}", Name = "DeleteDocument")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteDocument(int id)
        {
            var command = new DeleteDocumentCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}