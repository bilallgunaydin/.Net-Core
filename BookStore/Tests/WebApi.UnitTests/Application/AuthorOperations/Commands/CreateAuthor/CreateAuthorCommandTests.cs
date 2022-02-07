using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandTest(IMapper mapper, BookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [Fact]

        public void WhenAlreadyExistAuthorFirstNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author(){FirstName="Test_WhenAlreadyExistAuthorFirstNameIsGiven_InvalidOperationException_ShouldBeReturn", LastName="Şimşek", BirthDate=new DateTime(1990,01,10), BookId=1};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            command.Model= new CreateAuthorModel(){FirstName=author.FirstName};

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }

        [Fact]

        public void WhenValidİnputAreGiven_Author_ShouldBeCreate()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context,_mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {FirstName="Ahmet", LastName="Şükrü", BirthDate=DateTime.Now.AddYears(-10),BookId=1};
            command.Model=model;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var author= _context.Authors.SingleOrDefault(author=> author.FirstName==model.FirstName);
            author.Should().NotBeNull();
            author.BookId.Should().Be(model.BookId);
            author.BirthDate.Should().Be(model.BirthDate);
            author.LastName.Should().Be(model.LastName);
        }
    }
}