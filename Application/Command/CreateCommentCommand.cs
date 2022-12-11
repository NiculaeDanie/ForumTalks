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
    public class CreateCommentCommand: IRequest
    {
        public string Text { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
    }
    public class CreateCommentCommandHandler: IRequestHandler<CreateCommentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                Id = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                LikeCount = 0,
                PostId = request.PostId,
                Date = DateTime.Now,
                Text = request.Text
            };
            await _unitOfWork.CommentRepository.add(comment);
            await _unitOfWork.Save();
            return new Unit();
        }
    }
}
