using Xunit;
using Tests.TestSetup;
using WebApi.DBOperations;
using System.Linq;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using System;
using FluentAssertions;

namespace Tests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]       
        public void WhenANonExistentBookIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId=4;

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeDeleted()
        {
            var book = _context.Books.SingleOrDefault(book=> book.Id==3);
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId=book.Id;
            
            FluentActions.Invoking(()=> command.Handle()).Invoke();
            
            var deletedBook = _context.Books.SingleOrDefault(deletedBook=> deletedBook.Id==book.Id);
            
            deletedBook.Should().BeNull();

        }
    }
}