using Xunit;
using WebApi.DBOperations;
using System;
using FluentAssertions;
using Tests.TestSetup;
using System.Linq;
using WebApi.Application.BookOperations.Commands.UpdateBook;

namespace Tests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]       
        public void WhenANonExistentBookIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId=4;

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı!");
        }

        [Fact]
        public void WhenGivenTheSameValues_InvalidOperationException_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            var book = _context.Books.SingleOrDefault(book=> book.Id==3);
            command.BookId=book.Id;
            UpdateBookModel Model=new UpdateBookModel
            {
                Title="Martin Eden",
                GenreId=1
            };
            command.Model=Model;
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir kitap zaten mevcut!");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            var book = _context.Books.SingleOrDefault(book=> book.Id==3);
            UpdateBookModel Model = new UpdateBookModel
            {
                Title = "Updated Title",
                GenreId = 2
            };
            command.BookId=book.Id;
            command.Model=Model;
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            book.Should().NotBeNull();
            book.Title.Should().Be(Model.Title);
            book.GenreId.Should().Be(Model.GenreId);

        }

    }
}