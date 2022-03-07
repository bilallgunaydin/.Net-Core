using System;
using System.Linq;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]       
        public void WhenANonExistentAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId=4;

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı!");
        }

        [Fact]
        public void WhenGivenTheSameValues_InvalidOperationException_ShouldBeReturn()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            var author = _context.Authors.SingleOrDefault(author=> author.Id==3);
            command.AuthorId=author.Id;
            UpdateAuthorModel Model=new UpdateAuthorModel
            {
               FirstName="Jack",
               LastName="London",
               BirthDate=DateTime.Parse("1900-09-01"),
               BookId=1
            };
            command.Model=Model;
            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı yazar zaten mevcut!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            var author = _context.Authors.SingleOrDefault(author=> author.Id==1);
            UpdateAuthorModel Model = new UpdateAuthorModel
            {
               FirstName="Uptated FirstName",
               LastName="Uptated LastName",
               BirthDate=DateTime.Parse("1901-09-01"),
               BookId=1

            };
            command.AuthorId=author.Id;
            command.Model=Model;
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            author.Should().NotBeNull();
            author.FirstName.Should().Be(Model.FirstName);
            author.LastName.Should().Be(Model.LastName);
            author.BirthDate.Should().Be(Model.BirthDate);
            author.BookId.Should().Be(Model.BookId);

        }

    }
}