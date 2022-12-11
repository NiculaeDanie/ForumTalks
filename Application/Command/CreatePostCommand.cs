using Application.Abstracts;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public class CreatePostCommand: IRequest
    {
        public string Title { get; set; }
        public string Descriprion { get; set; }
        public string OwnerId { get; set; }
        public string ForumId { get; set; }
    }
    public class CreatePostCommandHandler: IRequestHandler<CreatePostCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.Title,
                Description = request.Descriprion,
                Created = DateTime.Now,
                OwnerId = request.OwnerId,
                ForumId = request.ForumId,
            };
            await _unitOfWork.PostRepository.add(post);
            await _unitOfWork.Save();
            return new Unit();
        }
    }
}
