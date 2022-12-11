using Application.Command;
using Application.Query;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForumTalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CommentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        // GET: api/<Comment>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var command = new GetAllCommentQuery
            {
                PostId = id
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<Comment>
        [HttpPost("{ownerId}/{id}")]
        public async Task<IActionResult> Post(string ownerId, string id, [FromBody] string value)
        {
            var command = new CreateCommentCommand
            {
                PostId = id,
                UserId = ownerId,
                Text = value
            };
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();
            return Ok();
        }

        // PUT api/<Comment>/5
        [HttpPut("{ownerId}/{id}")]
        public async Task<IActionResult> Put(string ownerId, string id, [FromBody] string value)
        {
            var command = new UpdateCommentCommand
            {
                Id = id,
                OwnerId = ownerId,
                Text = value
            };
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();
            return Ok();
        }

        // DELETE api/<Comment>/5
        [HttpDelete("{ownerId}/{id}")]
        public async Task<IActionResult> Delete(string ownerId,string id)
        {
            var command = new DeleteCommentCommand
            {
                Id = id,
                OwnerId = ownerId
            };
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();
            return Ok();
        }
    }
}
