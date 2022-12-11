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
    public class UpdateCommentCommand: IRequest<Comment>
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string Text { get; set; }
    }
    public class UpdateCommentCommandHandler: IRequestHandler<UpdateCommentCommand, Comment>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Comment> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.getById(request.Id);
            if (comment.UserId != request.OwnerId)
                return null;
            comment.Text = request.Text;
            await _unitOfWork.CommentRepository.update(comment);
            await _unitOfWork.Save();
            return comment;

        }
    }
}
