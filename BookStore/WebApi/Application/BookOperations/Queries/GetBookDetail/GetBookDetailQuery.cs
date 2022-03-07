using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public int BookId {get; set; }
        public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _dbContext.Books.Include(x=> x.Genre).Where(book=> book.Id==BookId).SingleOrDefault();
            if(book is null)
            throw new InvalidOperationException("Kitap Bulunamadı!");

            BookDetailViewModel vm=_mapper.Map<BookDetailViewModel>(book); //new BookDetailViewModel();
            return vm;
        }
    }

    public class BookDetailViewModel
    {
        public string Title {get; set;}

        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}