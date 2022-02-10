using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
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
            
            if(_dbContext.Authors.Any(x=> x.FirstName.ToLower()==Model.FirstName.ToLower() 
                && x.LastName.ToLower()==Model.LastName.ToLower()
                && x.BirthDate==Model.BirthDate
                && x.BookId==Model.BookId))
                throw new InvalidOperationException("Aynı yazar zaten mevcut!");


            author.BookId=Model.BookId != default? Model.BookId : author.BookId;
            author.FirstName=Model.FirstName !=default ? Model.FirstName : author.FirstName;
            author.LastName=Model.LastName !=default ? Model.LastName : author.LastName;
            author.BirthDate=Model.BirthDate !=default ? Model.BirthDate : author.BirthDate;

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