using Application.Command;
using Application.Query;
using AutoMapper;
using ForumTalks.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
        [HttpGet("{id}/{userId}")]
        public async Task<IActionResult> Get(string id, string userId)
        {
            var command = new GetAllCommentQuery
            {
                PostId = id,
                UserId = userId
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<Comment>
        [HttpPost("{ownerId}/{id}")]
        public async Task<IActionResult> Post(string ownerId, string id, [FromBody] CommentDto value)
        {
            var command = new CreateCommentCommand
            {
                PostId = id,
                UserId = ownerId,
                Text = value.Text
            };
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // PUT api/<Comment>/5
        [HttpPut("{ownerId}/{id}")]
        public async Task<IActionResult> Put(string ownerId, string id, [FromBody] CommentDto value)
        {
            var command = new UpdateCommentCommand
            {
                Id = id,
                OwnerId = ownerId,
                Text = value.Text
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

        [HttpPost]
        [Route("picture/{userId}")]
        public async Task<IActionResult> Picture(string userId, [FromForm] IFormFile model)
        {
            await _mediator.Send(new UploadPictureCommand(){ id = userId, formFile = model });

            return Ok();
        }

        [HttpGet]
        [Route("picture/{userId}")]
        public async Task<IActionResult> PictureGet(string userId)
        {
            var result = await _mediator.Send(new GetPictureQuery() { userId = userId});
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("like/{commentId}/{userId}")]
        public async Task<IActionResult> Like(string userId, string commentId)
        {
            await _mediator.Send(new LikeCommentCommand() { UserId = userId, CommentId = commentId });

            return Ok();
        }
    }
}
