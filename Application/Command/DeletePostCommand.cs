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
    public class DeletePostCommand: IRequest<Post>
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
    }
    public class DeletePostCommandHandler: IRequestHandler<DeletePostCommand, Post>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeletePostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostRepository.getById(request.Id);
            if (post == null || post.OwnerId != request.OwnerId)
                return null;
            await _unitOfWork.PostRepository.delete(post);
            await _unitOfWork.Save();
            return post;
        }
    }
}
