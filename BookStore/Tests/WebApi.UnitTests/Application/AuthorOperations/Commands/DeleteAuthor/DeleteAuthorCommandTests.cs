using System;
using System.Linq;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.DeleteBook
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]       
        public void WhenANonExistentAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId=4;

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı!");
        }

        [Fact]       
        public void WhenIfTheBookObjectIsNotNull_InvalidOperationException_ShouldBeReturn()
        {
            var author = _context.Authors.SingleOrDefault(author=> author.Id==3);
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId=author.Id;
            

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazara ait bir kitap var, silemezsiniz.");
        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeDeleted()
        {
            var author = _context.Authors.SingleOrDefault(author=> author.Id==3);
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            author.Book=null;
            command.AuthorId=author.Id;
            
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            
            var deletedBook = _context.Authors.SingleOrDefault(deletedBook=> deletedBook.Id==author.Id);
            
            deletedBook.Should().BeNull();

        }
    }
}