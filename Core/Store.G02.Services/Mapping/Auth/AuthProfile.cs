using AutoMapper;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Shard.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Mapping.Auth
{
    internal class AuthProfile  : Profile
    {
        public AuthProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
