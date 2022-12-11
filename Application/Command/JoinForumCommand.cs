using Application.Abstracts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public class JoinForumCommand: IRequest
    {
        public string ForumId { get; set; }
        public string UserId { get; set; }
    }
    public class JoinForumCommandHandler: IRequestHandler<JoinForumCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> userManager;
        public JoinForumCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(JoinForumCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            var forum = await _unitOfWork.ForumRepository.getById(request.ForumId);
            forum.Users.Add(new UserForum { Id = Guid.NewGuid().ToString(), ForumId = request.ForumId, UserId = user.Id , user = user, Forum = forum});
            await _unitOfWork.ForumRepository.update(forum);
            await _unitOfWork.Save();
            return new Unit();
        }
    }
}
