using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using National_Park_API.Models;
using National_Park_API.Models.DTos;
using National_Park_API.Repository.IRepository;

namespace National_Park_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailController : Controller
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;
        private readonly INationalParkRepository _nationalParkRepository; // ADD

        public TrailController (ITrailRepository trailRepository, IMapper mapper, INationalParkRepository nationalParkRepository)
        {
            _trailRepository = trailRepository;
            _mapper = mapper;
            _nationalParkRepository = nationalParkRepository;
        }
        [HttpGet]
        public IActionResult GetTrails()
        {
            return Ok(_trailRepository.GetTrails().Select(_mapper.Map<TrailDTo>));
        }
        [HttpGet("{id:int}", Name = "GetTrail")]
        public IActionResult GetTrail(int trailId)
        {
            var trail = _trailRepository.GetTrail(trailId);
            if (trail == null) return NotFound();
            var trailDto = _mapper.Map<TrailDTo>(trail);
            return Ok(trailDto);
        }
        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailDTo trailDto)
        {
            if (trailDto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var trail = _mapper.Map<Trail>(trailDto);
            if (_trailRepository.TrailExists(trail.Name))
            {
                ModelState.AddModelError("", "Trail already exists!");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            if (!_trailRepository.CreateTrail(trail))
            {
                ModelState.AddModelError("", "Something went wrong while saving: " + trail.Name);
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { id = trail.Id }, trail);
        }
        [HttpPut("{trailId:int}")]
        public IActionResult UpdateTrail([FromBody] TrailDTo trailDto)
        {
            if (trailDto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var trail = _mapper.Map<Trail>(trailDto);
            if (!_trailRepository.TrailExists(trail.Id))
            {
                ModelState.AddModelError("", "Trail doesn't exist!");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            if (!_trailRepository.UpdateTrail(trail))
            {
                ModelState.AddModelError("", "Something went wrong while updating: " + trail.Name);
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{trailId:int}")]
        public IActionResult DeleteTrail(int trailId)
        {
            if (!_trailRepository.TrailExists(trailId)) return NotFound();

            var trail = _trailRepository.GetTrail(trailId);
            if (trail == null) return NotFound();
            if (!_trailRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", "Something went wrong while deleting: " + trail.Name);
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return NoContent();
        }
    }
}
