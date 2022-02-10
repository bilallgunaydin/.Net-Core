using Xunit;
using Tests.TestSetup;
using WebApi.DBOperations;
using System;
using FluentAssertions;
using AutoMapper;
using System.Linq;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

namespace Tests.Application.AuthorOperations.Queries.GetBookDetail
{
    public class GetAuthorDetailTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenANonExistentAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorId=4;

            FluentActions
                .Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı!");
        }

        [Fact]
        public void WhenValidİnputsAreGiven_Author_ShouldBeListed()
        { 
            GetAuthorDetailQuery command = new GetAuthorDetailQuery(_context, _mapper);
            command.AuthorId=3;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var author =_context.Authors.SingleOrDefault(author=> author.Id==command.AuthorId);
            author.Should().NotBeNull();

        }
    }
}