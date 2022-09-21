using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Features.Languages.Commands.DeleteLanguage;
using Application.Features.Languages.Dtos;
using Application.Features.Languages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile
{
    public class DeleteGitHubProfileCommand : IRequest<DeletedGitHubProfileDto>
    {
        public int Id { get; set; }
        public class DeleteGitHubProfileCommandHandler : IRequestHandler<DeleteGitHubProfileCommand, DeletedGitHubProfileDto>
        {

            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

            public DeleteGitHubProfileCommandHandler(IGitHubProfileRepository _gitHubProfileRepository, IMapper mapper, GitHubProfileBusinessRules gitHubProfileBusinessRules)
            {
                _gitHubProfileRepository = _gitHubProfileRepository;
                _mapper = mapper;
                _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
            }

            public async Task<DeletedGitHubProfileDto> Handle(DeleteGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                await _gitHubProfileBusinessRules.GitHubProfileShouldExistWhenDeleted(request.Id);

                GitHubProfile? gitHubProfile = await _gitHubProfileRepository.GetAsync(l => l.Id == request.Id);
                GitHubProfile deletedGitHubProfile = await _gitHubProfileRepository.DeleteAsync(gitHubProfile!);
                DeletedGitHubProfileDto deletedGitHubProfileDto = _mapper.Map<DeletedGitHubProfileDto>(deletedGitHubProfile);
                return deletedGitHubProfileDto;
            }
        }
    }
}
