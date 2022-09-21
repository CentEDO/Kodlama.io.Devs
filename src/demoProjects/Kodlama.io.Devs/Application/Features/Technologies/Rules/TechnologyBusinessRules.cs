using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }
        public async Task TechnologyNameCanNotBeDuplicatedWhenInsertedOrUpdated(string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(l => l.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name exists!");
        }
        public async Task TechnologyShouldExistWhenRequested(Technology technology)
        {
            if (technology == null) throw new BusinessException("Requested technology does not exists!");
        }
        public async Task TechnologyShouldExistWhenDeleted(int id)
        {
            Technology? technology = await _technologyRepository.GetAsync(l => l.Id == id);
            if (technology == null) throw new BusinessException("Requested technology does not exists!");
        }
    }
}
