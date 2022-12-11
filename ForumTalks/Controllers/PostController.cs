using Application.Command;
using Application.Query;
using AutoMapper;
using ForumTalks.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForumTalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PostController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/<ForumController>
        [HttpGet("all/{forumId}")]
        public async Task<IActionResult> Get(string forumId)
        {
            var result = await _mediator.Send(new GetAllPostQuery() { ForumId = forumId });
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        // GET api/<ForumController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetPostByIdQuery() { Id = id });
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        // POST api/<ForumController>
        [HttpPost("{ownerId}/{forumId}")]
        public async Task<IActionResult> Post(string ownerId, string forumId, [FromBody] PostPutDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new CreatePostCommand
            {
                Title = value.Title,
                Descriprion = value.Description,
                ForumId = forumId,
                OwnerId = ownerId,
            };
            var result = await _mediator.Send(command);

            return Ok();
        }

        // PUT api/<ForumController>/5
        [HttpPut("{ownerId}/{id}")]
        public async Task<IActionResult> Put(string ownerId, string id, [FromBody] PostPutDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new UpdatePostCommand
            {
                Id = id,
                Title = value.Title,
                Description = value.Description,
                OwnerId = ownerId,
            };
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE api/<ForumController>/5
        [HttpDelete("{senderId}/{id}")]
        public async Task<IActionResult> Delete(string senderId, string id)
        {
            var command = new DeletePostCommand
            {
                Id = id,
                OwnerId = senderId,
            };
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
