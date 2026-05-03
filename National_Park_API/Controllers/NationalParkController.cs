using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using National_Park_API.Models;
using National_Park_API.Models.DTos;
using National_Park_API.Repository.IRepository;

namespace National_Park_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            _nationalParkRepository = nationalParkRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var nationalParkDtoList = _nationalParkRepository.GetNationalParks().Select(_mapper.Map<NationalParkDto>);
            return Ok(nationalParkDtoList);
        }
        [HttpGet("{id:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int nationalparkid)
        {
            var nationalPark = _nationalParkRepository.GetNationalPark(nationalparkid);
            if (nationalPark == null) return NotFound();

            var nationalParkDto = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(nationalParkDto);
        }
        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var nationalPark = _mapper.Map<National_Park>(nationalParkDto);
            if (_nationalParkRepository.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park already exists!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (!_nationalParkRepository.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", "Something went wrong while saving: " + nationalPark.Name);
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return CreatedAtRoute("GetNationalPark", new { id = nationalPark.Id }, nationalPark);

        }
        [HttpPut]
        public IActionResult UpdateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var nationalPark = _mapper.Map<National_Park>(nationalParkDto);
            if (!_nationalParkRepository.NationalParkExists(nationalPark.Id))
                return NotFound();
            if (!_nationalParkRepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", "Something went wrong while update NP!! " + nationalPark.Name);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpDelete("{nationalParkId:int}")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalParkRepository.NationalParkExists(nationalParkId))
                return NotFound();

            var nationalPark = _nationalParkRepository.GetNationalPark(nationalParkId);

            if (nationalPark == null) return NotFound();

            if (!_nationalParkRepository.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", "Something went wrong while deleting NP!! " + nationalPark.Name);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }

}
