﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Product.Core.Entities;
using Product.Core.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastrucre.Repository
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenServices(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["token:Key"]));
        }

        public string CreateToken(AppUser appUser)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
            var Claims = new List<Claim>()
            {
                new Claim (JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim (JwtRegisteredClaimNames.GivenName, appUser.DisplayName)
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddDays(10),
                Issuer = _config["Token:Issuer"],
                SigningCredentials = creds,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
