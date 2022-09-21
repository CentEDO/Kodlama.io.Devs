using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Rules
{
    public class GitHubProfileBusinessRules
    {
        private readonly IGitHubProfileRepository _gitHubProfileRepository;

        public GitHubProfileBusinessRules(IGitHubProfileRepository gitHubProfileRepository)
        {
            _gitHubProfileRepository = gitHubProfileRepository;
        }
        public async Task ProfileUrlCanNotBeDuplicatedWhenInsertedOrUpdated(string profileUrl)
        {
            IPaginate<GitHubProfile> result = await _gitHubProfileRepository.GetListAsync(l => l.ProfileUrl == profileUrl);
            if (result.Items.Any()) throw new BusinessException("That profile already exists!");
        }
        public async Task GitHubProfileShouldExistWhenDeleted(int id)
        {
            GitHubProfile? gitHubProfile = await _gitHubProfileRepository.GetAsync(l => l.Id == id);
            if (gitHubProfile == null) throw new BusinessException("Requested GitHub profile does not exists!");
        }
        public async Task GitHubProfileShouldExistWhenRequested(GitHubProfile gitHubProfile)
        {
            if (gitHubProfile == null) throw new BusinessException("Requested profile does not exists!");
        }
    }
}
