using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        // private readonly BookStoreDbContext _dbContext;
        private readonly IBookStoreDbContext _dbContext;

        public UpdateAuthorModel Model {get; set;}
        
        public int AuthorId {get; set;}

        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author= _dbContext.Authors.SingleOrDefault(x=> x.Id==AuthorId);
            if(author is null)
            throw new InvalidOperationException("Yazar bulunamadı!");

            author.BookId=Model.BookId != default? Model.BookId : author.BookId;
            author.FirstName=Model.FirstName !=default ? Model.FirstName : author.FirstName;
            author.LastName=Model.LastName !=default ? Model.LastName : author.LastName;

            _dbContext.SaveChanges();
        }

    }

    public class UpdateAuthorModel
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTime BirthDate {get; set;}
        public int BookId {get; set;}
    }
}