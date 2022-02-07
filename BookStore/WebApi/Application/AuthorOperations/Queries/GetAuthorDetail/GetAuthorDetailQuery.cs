using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId {get; set;}
        // private readonly BookStoreDbContext _context;
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            // var book = _dbContext.Books.Include(x=> x.Genre).Where(book=> book.Id==BookId).SingleOrDefault();
            // var book = _dbContext.Books.Include(x=> x.Genre).Where(book=> book.Id==BookId).SingleOrDefault();
            var author = _context.Authors.Include(x=>x.Book).Where(author=> author.BookId==AuthorId).FirstOrDefault();
            if(author is null)
            throw new InvalidOperationException("Yazar Bulunamadı!");

            return _mapper.Map<AuthorDetailViewModel>(author);
        }
        

    }

    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public string Book {get; set;}
    }
}