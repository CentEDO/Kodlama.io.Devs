using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Dtos;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.JWT;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Developers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Developer, UserForRegisterDto>().ReverseMap();
            CreateMap<UserForRegisterDto, CreateDeveloperCommand>().ReverseMap();
            CreateMap<TokenDto, AccessToken>().ReverseMap();
            CreateMap<Developer, UserForLoginDto>().ReverseMap();
            CreateMap<UserForLoginDto, CreateDeveloperCommand>().ReverseMap();
        }

    }
}
