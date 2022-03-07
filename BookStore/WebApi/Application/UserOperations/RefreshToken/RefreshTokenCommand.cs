using System;
using System.Linq;
using WebApi.DBOperations;
using Microsoft.Extensions.Configuration;
using WebApi.TokenOperations.Models;
using WebApi.TokenOperations;

namespace WebApi.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken {get; set;}
        private readonly IBookStoreDbContext _dbContext;

        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x=> x.RefreshToken ==RefreshToken  && x.RefreshTokenExpireDate>DateTime.Now);
            if(user is not null)
            {
                //Token Yarat
                TokenHandler handler = new TokenHandler(_configuration);
                Token token=handler.CreateAccessToken(user);

                user.RefreshToken=token.RefreshToken;
                user.RefreshTokenExpireDate=token.Expiration.AddMinutes(5);
                _dbContext.SaveChanges();

                return token;
            }
            else
            throw new InvalidOperationException("Valid bir Refresh Token Bulunamadı!");
        }

        
    }
}

        