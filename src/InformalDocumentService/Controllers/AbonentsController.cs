using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using InformalDocumentService.Data.Repositories;
using InformalDocumentService.Dtos;

namespace InformalDocumentService.Controllers
{
    [Route("api/u/[controller]")]
    [ApiController]
    public class AbonentsController : ControllerBase
    {
        private readonly IDocumentRepo _repository;
        private readonly IMapper _mapper;

        public AbonentsController(IDocumentRepo documentRepo, IMapper mapper)
        {
            _repository = documentRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AbonentReadDto>> GetAllAbonents()
        {
            Console.WriteLine("DEBUG. Getting all abonents");

            var abonents = _repository.GetAllAbonents();

            return Ok(_mapper.Map<IEnumerable<AbonentReadDto>>(abonents));
        }

        [HttpPost]
        public IActionResult TestInboundConnection()
        {
            Console.WriteLine("DEBUG. Inbound POST # AbonentService");

            return Ok("Inbound test from Abonents Controller");
        }
    }
}
