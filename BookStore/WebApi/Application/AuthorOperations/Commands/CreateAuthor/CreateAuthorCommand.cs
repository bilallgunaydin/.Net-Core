using AutoMapper;
using System;
using System.Linq;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApi.DBOperations;

namespace  WebApi.Application.AuthorOperations.Commands.CreateAuthor
{ 
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model {get; set;}
        private readonly IBookStoreDbContext _dbContext;

        // private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        private readonly ILazyLoader _lazyLoader;
        
        public CreateAuthorCommand(ILazyLoader lazyLoader)
        {
             _lazyLoader = lazyLoader;
        }
        public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            
        }

        public void Handle()
        {
            var author=_dbContext.Authors.SingleOrDefault(x=> x.FirstName==Model.FirstName);
            if(author is not null)
            throw new InvalidOperationException("Yazar zaten mevcut");

            author=_mapper.Map<Author>(Model);

            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTime BirthDate {get; set;}
        public int BookId {get; set;}
        public Book Book {get; set;}
    }
    
}