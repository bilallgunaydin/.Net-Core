using Xunit;
using Tests.TestSetup;
using WebApi.DBOperations;
using System;
using FluentAssertions;
using AutoMapper;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using System.Linq;

namespace Tests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public void WhenANonExistentBookIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId=4;

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı!");
        }

        [Fact]
        public void WhenValidİnputsAreGiven_Book_ShouldBeListed()
        {
            GetBookDetailQuery command = new GetBookDetailQuery(_context, _mapper);
            command.BookId=3;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var book =_context.Books.SingleOrDefault(book=> book.Id==command.BookId);
            book.Should().NotBeNull();

        }
    }
}