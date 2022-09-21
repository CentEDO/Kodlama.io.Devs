using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Rules;
using Application.Features.Languages.Commands.UpdateLanguage;
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

namespace Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile
{
    public class UpdateGitHubProfileCommand : IRequest<UpdatedGitHubProfileDto>
    {
        public int Id { get; set; }
        public string ProfileUrl { get; set; }

        public class UpdateGitHubProfileCommandHandler : IRequestHandler<UpdateGitHubProfileCommand, UpdatedGitHubProfileDto>
        {
            private readonly IGitHubProfileRepository _gitHubProfileRepository;
            private readonly IMapper _mapper;
            private readonly GitHubProfileBusinessRules _gitHubProfileBusinessRules;

            public UpdateGitHubProfileCommandHandler(IGitHubProfileRepository gitHubProfileRepository, IMapper mapper, GitHubProfileBusinessRules gitHubProfileBusinessRules)
            {
                _gitHubProfileRepository = gitHubProfileRepository;
                _mapper = mapper;
                _gitHubProfileBusinessRules = gitHubProfileBusinessRules;
            }

            public async Task<UpdatedGitHubProfileDto> Handle(UpdateGitHubProfileCommand request, CancellationToken cancellationToken)
            {
                GitHubProfile? gitHubProfile = await _gitHubProfileRepository.GetAsync(l => l.Id == request.Id);
                await _gitHubProfileBusinessRules.GitHubProfileShouldExistWhenRequested(gitHubProfile!);
                gitHubProfile.ProfileUrl = request.ProfileUrl;
                GitHubProfile updatedGitHubProfile = await _gitHubProfileRepository.UpdateAsync(gitHubProfile);
                UpdatedGitHubProfileDto updatedGitHubProfileDto = _mapper.Map<UpdatedGitHubProfileDto>(updatedGitHubProfile);
                await _gitHubProfileBusinessRules.ProfileUrlCanNotBeDuplicatedWhenInsertedOrUpdated(request.ProfileUrl);

                return updatedGitHubProfileDto;
            }
        }
    
    }
}
