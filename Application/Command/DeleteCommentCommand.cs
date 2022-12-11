using Application.Abstracts;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public class DeleteCommentCommand: IRequest<Comment>
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
    }
    public class DeleteCommentCommandHandler: IRequestHandler<DeleteCommentCommand, Comment>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Comment> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.CommentRepository.getById(request.Id);
            if ( comment == null )
            {
                return null;
            }
            await _unitOfWork.CommentRepository.delete(comment);
            await _unitOfWork.Save();
            return comment;
        }
    }
}
