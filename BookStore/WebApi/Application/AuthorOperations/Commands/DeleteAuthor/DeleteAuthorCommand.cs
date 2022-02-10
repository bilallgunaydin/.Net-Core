using System.Linq;
using System;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {

        private readonly IBookStoreDbContext _dbContext;
        public int AuthorId {get; set;}
        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author= _dbContext.Authors.SingleOrDefault(x=> x.Id==AuthorId);
            
            if(author is null)
            throw new InvalidOperationException("Yazar Bulunamadı!");

            if(author.Book is not null)
            throw new InvalidOperationException("Yazara ait bir kitap var, silemezsiniz.");

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
            
        }
    }
}