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
    public class UpdatePostCommand : IRequest<Post>
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
    }
    public class UpdatePostCommandHandler: IRequestHandler<UpdatePostCommand,Post>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.PostRepository.getById(request.Id);
            if (post == null || post.OwnerId != request.OwnerId)
                return null;
            post.Description = request.Description;
            post.Title = request.Title;
            await _unitOfWork.PostRepository.update(post);
            await _unitOfWork.Save();
            return post;
        }
    }
}
