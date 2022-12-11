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
    public class DeleteForumCommand: IRequest<Forum>
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
    }
    public class DeleteForumCommandHandler: IRequestHandler<DeleteForumCommand, Forum>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteForumCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Forum> Handle(DeleteForumCommand request, CancellationToken cancellationToken)
        {
            var forum = await _unitOfWork.ForumRepository.getById(request.Id);
            if (forum == null || request.OwnerId != forum.OwnerId)
                return null;
            await _unitOfWork.ForumRepository.delete(forum);
            await _unitOfWork.Save();
            return forum;
        }
    }
}
