using Application.Command;
using Application.Query;
using AutoMapper;
using Domain;
using ForumTalks.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForumTalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ForumController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // GET: api/<ForumController>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllForumQuery());
            if(result == null)
            {
                return NoContent();
            }
            foreach (var res in result)
            {
                foreach (var forum in res.Users)
                {
                    forum.Forum = null;
                }
            }
            return Ok(result);
        }

        [HttpGet("owned/{ownerId}")]
        public async Task<IActionResult> Get(string ownerId)
        {
            var result = await _mediator.Send(new GetAllForumQuery());
            if (result == null)
            {
                return NoContent();
            }
            foreach(var res in result)
            {
                foreach(var forum in res.Users)
                {
                    forum.Forum = null;
                }
            }
            result = result.Where(x => x.Users.Any(us => us.UserId == ownerId)).ToList();
            return Ok(result);
        }

        // GET api/<ForumController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(string id)
        {
            var result = await _mediator.Send(new GetForumByIdQuery() { Id = id});
            if (result == null)
            {
                return NoContent();
            }
            foreach (var forum in result.Users)
            {
                forum.Forum = null;
            }
            return Ok(result);
        }

        [HttpGet("joinforum/{userId}/{id}")]
        public async Task<IActionResult> Join(string userId,string id)
        {
            await _mediator.Send(new JoinForumCommand() { ForumId = id, UserId = userId });
            return Ok();
        }

        // POST api/<ForumController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ForumPutDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new CreateForumCommand
            {
                Name = value.Name,
                Description = value.Description,
                OwnerId = value.OwnerId,
                Price = value.Price
            };
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        // PUT api/<ForumController>/5
        [HttpPut("{ownerId}/{id}")]
        public async Task<IActionResult> Put(string ownerId, string id, [FromBody] ForumPutDto value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = new UpdateForumCommand
            {
                Id = id,
                Name = value.Name,
                Description = value.Description,
                OwnerId = ownerId,
                Price = value.Price
            };
            var result = await _mediator.Send(command);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok();
        }

        // DELETE api/<ForumController>/5
        [HttpDelete("{senderId}/{id}")]
        public async Task<IActionResult> Delete(string senderId, string id)
        {
            var command = new DeleteForumCommand
            {
                Id = id,
                OwnerId = senderId,
            };
            var result = await _mediator.Send(command);
            if( result == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
