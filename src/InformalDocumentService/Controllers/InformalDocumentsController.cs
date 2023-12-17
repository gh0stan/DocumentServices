using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using InformalDocumentService.Dtos;
using InformalDocumentService.Data.Models;
using InformalDocumentService.Data.Repositories;
using InformalDocumentService.AsyncDataServices;

namespace InformalDocumentService.Controllers
{
    [Route ("api/u/[controller]/[action]")]
    [ApiController]
    public class InformalDocumentsController : ControllerBase
    {
        private readonly IDocumentRepo _repository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public InformalDocumentsController(IDocumentRepo documentRepo, IMapper mapper, IMessageBusClient messageBusClient) 
        {
            _repository = documentRepo; 
            _mapper = mapper;
            _messageBusClient = messageBusClient;
    }

        /// <summary>
        /// Getting all documents sent by abonent.
        /// </summary>
        /// <param name="abonentId">Sender abonent Id.</param>
        /// <returns>Returns list of documents <see cref="DocumentReadDto"/>.</returns>
        [HttpGet("{abonentId}", Name = "GetAllSentDocuments")]
        public ActionResult<IEnumerable<DocumentReadDto>> GetAllSentDocuments(int abonentId) 
        {
            Console.WriteLine($"DEBUG. Getting all sent documents for abonent: {abonentId}.");

            if (!_repository.AbonentExists(abonentId)) 
            {
                return NotFound();
            }

            var documents = _repository.GetAllSentDocuments(abonentId);

            return Ok(_mapper.Map<IEnumerable<DocumentReadDto>>(documents));
        }

        /// <summary>
        /// Getting a document by document guid.
        /// </summary>
        /// <param name="documentGuid">Document guid.</param>
        /// <returns>Returns requested document <see cref="DocumentReadDto"/>.</returns>
        [HttpGet("{documentGuid}", Name = "GetDocumentByGuid")]
        public ActionResult<DocumentReadDto> GetDocumentByGuid(string documentGuid)
        {
            Console.WriteLine("DEBUG. Getting document by guid.");

            var document = _repository.GetDocumentByGuid(documentGuid);
            if (document != null)
            {
                return Ok(_mapper.Map<DocumentReadDto>(document));
            }

            return NotFound();
        }

        /// <summary>
        /// Getting a document by Id.
        /// </summary>
        /// <param name="id">Document id in system.</param>
        /// <returns>Requested document <see cref="DocumentReadDto"/>.</returns>
        [HttpGet("{id}", Name = "GetDocumentById")]
        public ActionResult<DocumentReadDto> GetDocumentById(int id)
        {
            Console.WriteLine("DEBUG. Getting document by id.");

            var document = _repository.GetDocumentById(id);

            if (document != null)
            {
                return Ok(_mapper.Map<DocumentReadDto>(document));
            }

            return NotFound();
        }

        /// <summary>
        /// Creating a document by abonent.
        /// </summary>
        /// <param name="abonentId">Sender abonent Id.</param>
        /// <param name="documentCreateDto">Document representation <see cref="DocumentCreateDto"/>.</param>
        /// <returns>Returns created document uri.</returns>
        [HttpPost]
        public ActionResult<DocumentReadDto> CreateDocument(int abonentId, DocumentCreateDto documentCreateDto)
        {
            Console.WriteLine($"DEBUG. Creating document by abonent: {abonentId}.");

            if (_repository.AbonentExists(abonentId)) 
            {  
                return NotFound(); 
            }

            var documentModel = _mapper.Map<Document>(documentCreateDto);

            _repository.CreateDocument(abonentId, documentModel);
            _repository.SaveChanges();

            var documentReadDto = _mapper.Map<DocumentReadDto>(documentModel);

            // Sending async message
            try
            {
                var notificationCreateDto = new NotificationCreateDto();
                notificationCreateDto.ReceiverAbonentId = documentModel.ReceiverId;
                notificationCreateDto.Event = "Create_Notification";
                notificationCreateDto.Message = $"You have received new document of non-formalized type.";
                _messageBusClient.CreateNotification(notificationCreateDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR. Could not send notification asynchronously. {ex.Message}.");
            }

            if (documentReadDto != null)
            {
                return CreatedAtRoute(nameof(GetDocumentById), new { id = documentModel.Id } , documentReadDto );
            }

            return Problem("Failed to create document.");
        }
    }
}
