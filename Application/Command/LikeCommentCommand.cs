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
    public class LikeCommentCommand: IRequest
    {
        public string CommentId { get; set; }
        public string UserId {  get; set; }
    }
    public class LikeCommentCommandHandler: IRequestHandler<LikeCommentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public LikeCommentCommandHandler(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(LikeCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            var comment = await _unitOfWork.CommentRepository.getById(request.CommentId);
            if (comment.LikedUsers != null)
            {
                if (comment.LikedUsers.Any(x => x.UserId == user.Id))
                {
                    comment.LikeCount -= 1;
                    comment.LikedUsers.Remove(comment.LikedUsers.Where(x => x.UserId == user.Id).First());
                }
                else
                {
                    comment.LikeCount += 1;
                    comment.LikedUsers.Add(new UserComment() { Id = Guid.NewGuid().ToString(), Comment = comment, CommentId = comment.Id, User = user, UserId = request.UserId });
                }
                    
            }
            else
            {
                comment.LikeCount += 1;
                comment.LikedUsers = new List<UserComment>();
                comment.LikedUsers.Add(new UserComment() { Id = Guid.NewGuid().ToString(), Comment = comment, CommentId = comment.Id, User = user, UserId = request.UserId });
            }
                

            await _unitOfWork.CommentRepository.update(comment);
            await _unitOfWork.Save();
            return new Unit();
        }
    }
}
