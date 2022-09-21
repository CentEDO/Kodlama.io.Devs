using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Dtos;
using Application.Features.Developers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Commands.LoginDeveloper
{
    public class LoginDeveloperCommand:UserForLoginDto,IRequest<TokenDto>
    {
        public class LoginDeveloperCommandHandler : IRequestHandler<LoginDeveloperCommand, TokenDto>
        {
            private readonly IDeveloperRepository _developerRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly DeveloperBusinessRules _developerBusinessRules;

            public LoginDeveloperCommandHandler(IMapper mapper, ITokenHelper tokenHelper,
           IDeveloperRepository developerRepository, DeveloperBusinessRules developerBusinessRules)
           => (_mapper, _tokenHelper, _developerRepository, _developerBusinessRules) =
               (mapper, tokenHelper, developerRepository, developerBusinessRules);

            public async Task<TokenDto> Handle(LoginDeveloperCommand request, CancellationToken cancellationToken)
            {
                var developer = await _developerRepository.GetAsync(u => u.Email == request.Email);
                bool result = HashingHelper.VerifyPasswordHash(request.Password, developer.PasswordHash, developer.PasswordSalt);
                if (!result)
                {
                    throw new BusinessException("Credentials do not match");
                }

                var token = _tokenHelper.CreateToken(developer, new List<OperationClaim>());

                var createdToken = _mapper.Map<TokenDto>(token);

                return createdToken;
            }

        }
    }
}
