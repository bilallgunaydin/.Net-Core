using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Hundle()
        {
            var authorList= _context.Authors.Include(x=> x.Book).OrderBy(x=> x.Id).ToList();
            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(authorList);
            return returnObj;
        }

    }

    public class AuthorsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Book {get; set;}
    }
}